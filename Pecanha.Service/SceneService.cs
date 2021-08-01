using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;
using Pecanha.Domain.Enum;
using Pecanha.Service.Handlers;
using System;

namespace Pecanha.Service {
    public class SceneService : ServiceBase<Scene>, ISceneService {
        private readonly ISceneRepository _sceneRepository;
        private readonly IRecordHistoryRepository _recordRepository;
        private const string _msgNoNameProvided = "Nome não informado";
        private const string _msgFutureAlterNotAllowed = "Não é permitido inserir uma operação de alteração de estado no futuro";
        private const string _msgCantUndoOperation = "Não é permitido desfazer uma operação de alteração de estado que foi realizada há mais de 5 minutos.";
        private string _msgOpNotAllowed = "Não é permitido alterar diretamente de {0} para {1}";
        private string _msgInvalidState = "Não existe um estado com o identificador {0}";

        public SceneService(ISceneRepository repository, IRecordHistoryRepository recordRepository)
            : base(repository) {
            _sceneRepository = repository;
            _recordRepository = recordRepository;
        }

        public CommandResult Create(SceneCreateCommand sceneCommand) {
            try {
                var scene = sceneCommand.ToEntity(sceneCommand.Name);
                if (scene is null) {
                    return new CommandResult(false, false, _msgNoNameProvided, null);
                }
                _sceneRepository.Add(scene);
                _recordRepository.Add(new RecordHistory(scene.Id));

                return new CommandResult(true, false, string.Empty, scene);
            } catch (Exception ex) {
                return new CommandResult(false, true, ex.Message, null);
            }
        }

        /*         1. A operação de alteração de estado deve receber um parâmetro indicativo da
          hora de alteração. Isso significa que é possível realizar uma operação de
          alteração de estado no passado, mas, ainda assim, o diagrama de estados deve
          ser respeitado. Por exemplo, não deve ser possível inserir uma operação de
          alteração de estado para gravada antes de uma alteração de estado para
          preparada. */
        public CommandResult ChangeState(SceneUpdateCommand sceneCommand) {
            try {

                var result = _sceneRepository.GetById(sceneCommand.Id);
                if (result is null)
                    return result;

                var scene = result.Log as Scene;

                _recordRepository.Add(new RecordHistory(scene.Id, scene.State, sceneCommand.NextState, sceneCommand.OperationHour));
                scene.UpdateState(sceneCommand);

                if (scene.Erro == ErroEnum.FutureAlterNotAllowed)
                    return new CommandResult(false, false, _msgFutureAlterNotAllowed, null);
                else if (scene.Erro == ErroEnum.OpNotAllowed)
                    return new CommandResult(false, false, string.Format(_msgOpNotAllowed, scene.State, sceneCommand.NextState), null);
                else if (scene.Erro == ErroEnum.CantUndoOperation)
                    return new CommandResult(false, false, _msgCantUndoOperation, null);
                else if (scene.Erro == ErroEnum.InvalidState)
                    return new CommandResult(false, false, string.Format(_msgInvalidState, sceneCommand.NextState), null);

                _sceneRepository.Update(scene);

                //F5. Implementar mecanismo de tempo real para acompanhamento do estado atual da gravação.
                EmailHandler.SendEmail(scene);
                return new CommandResult(true, false, string.Empty, this);
            } catch (Exception ex) {
                return new CommandResult(false, true, ex.Message, null);
            }
        }
    }
}

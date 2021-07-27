using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;
using Pecanha.Domain.Enum;
using System;

namespace Pecanha.Service {
    public class SceneService : ServiceBase<Scene>, ISceneService {
        private readonly ISceneRepository _sceneRepository;
        private readonly IRecordHistoryRepository _recordRepository;
        private const string _msgNoNameProvided = "Nome não informado";
        private const string _msgFutureAlterNotAllowed = "Não é permitido inserir uma operação de alteração de estado no futuro";
        private const string _msgCantUndoOperation = "Não é permitido desfazer uma operação de alteração de estado que foi realizada há mais de 5 minutos.";
        private string _msgOpNotAllowed = "Não é permitido alterar diretamente de {0} para {1}";

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

        public CommandResult ChangeState(SceneUpdateCommand sceneCommand) {
            try {
                if (sceneCommand.OperationHour > DateTime.Now)
                    return new CommandResult(false, true, _msgFutureAlterNotAllowed, null);

                var scene = _sceneRepository.GetById(sceneCommand.Id).Log as Scene;

                if (sceneCommand.OperationHour.Subtract(scene.OperationHour).TotalMinutes > 5)
                    return new CommandResult(false, true, _msgCantUndoOperation, null);
                
                if (scene.State == StateEnum.Pendente && sceneCommand.NextState == StateEnum.Gravada) {
                    return new CommandResult(false, true, string.Format(_msgOpNotAllowed, StateEnum.Pendente, sceneCommand.NextState), null);
                } else if (scene.State == StateEnum.Pendurada && sceneCommand.NextState != StateEnum.Preparada) {
                    return new CommandResult(false, true, string.Format(_msgOpNotAllowed, scene.State, sceneCommand.NextState), null);
                } else if (scene.State == StateEnum.Gravada && sceneCommand.NextState != StateEnum.Preparada) {
                    return new CommandResult(false, true, string.Format(_msgOpNotAllowed, scene.State, sceneCommand.NextState), null);
                }

                _recordRepository.Add(new RecordHistory(scene.Id, scene.State, sceneCommand.NextState, sceneCommand.OperationHour));
                scene.UpdateState(sceneCommand.NextState, sceneCommand.OperationHour);
                _sceneRepository.Update(scene);

                // Fazer a notificação
                return new CommandResult(true, false, string.Empty, scene);
            } catch (Exception ex) {
                return new CommandResult(false, true, ex.Message, null);
            }
        }
    }
}

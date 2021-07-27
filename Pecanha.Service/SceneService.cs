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

        /*
                   1. A operação de alteração de estado deve receber um parâmetro indicativo da
            hora de alteração. Isso significa que é possível realizar uma operação de
            alteração de estado no passado, mas, ainda assim, o diagrama de estados deve
            ser respeitado. Por exemplo, não deve ser possível inserir uma operação de
            alteração de estado para gravada antes de uma alteração de estado para
            preparada.
       
         */
        public CommandResult ChangeState(SceneUpdateCommand sceneCommand) {
            try {
                //2. Não deve ser permitido inserir uma operação de alteração de estado no futuro.
                if (sceneCommand.OperationHour > DateTime.Now)
                    return new CommandResult(false, true, _msgFutureAlterNotAllowed, null);

                var scene = _sceneRepository.GetById(sceneCommand.Id).Log as Scene;

                if (scene.State == StateEnum.Pendente && sceneCommand.NextState == StateEnum.Gravada) {
                    return new CommandResult(false, true, string.Format(_msgOpNotAllowed, StateEnum.Pendente, sceneCommand.NextState), null);
                } else if (scene.State == StateEnum.Pendurada && sceneCommand.NextState != StateEnum.Preparada) {
                    return new CommandResult(false, true, string.Format(_msgOpNotAllowed, scene.State, sceneCommand.NextState), null);
                } else if (scene.State == StateEnum.Gravada && sceneCommand.NextState != StateEnum.Preparada) {
                    return new CommandResult(false, true, string.Format(_msgOpNotAllowed, scene.State, sceneCommand.NextState), null);
                }

                //3. Não deve ser permitido desfazer uma operação de alteração de estado que foi realizada há mais de 5 minutos. (opcional)
                if (sceneCommand.OperationHour.Subtract(scene.OperationHour).TotalMinutes > 5 && this.IsUndoOperation(scene.State, sceneCommand.NextState))
                    return new CommandResult(false, true, _msgCantUndoOperation, null);               

                _recordRepository.Add(new RecordHistory(scene.Id, scene.State, sceneCommand.NextState, sceneCommand.OperationHour));
                scene.UpdateState(sceneCommand.NextState, sceneCommand.OperationHour);
                _sceneRepository.Update(scene);

                //F5. Implementar mecanismo de tempo real para acompanhamento do estado atual da gravação.
                EmailHandler.SendEmail(scene);                
                return new CommandResult(true, false, string.Empty, scene);
            } catch (Exception ex) {
                return new CommandResult(false, true, ex.Message, null);
            }
        }

        private bool IsUndoOperation(StateEnum actual, StateEnum next) {
            return actual != StateEnum.Pendente && (next == StateEnum.Pendente || next == StateEnum.Preparada);
        }
    }
}

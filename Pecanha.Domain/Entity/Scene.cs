using Pecanha.Domain.Commands;
using Pecanha.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Pecanha.Domain.Entity {
    public class Scene : To {
        public string Name { get; private set; }
        public StateEnum State { get; private set; }
        public DateTime OperationHour { get; private set; }
        public ICollection<RecordHistory> RecordHistories { get; set; }
        public ErroEnum Erro { get; private set; }

        public Scene(string name) {
            Name = name;
            State = StateEnum.Pendente;
            OperationHour = DateTime.Now;
            Erro = ErroEnum.NoError;
        }

        public void UpdateState(SceneUpdateCommand sceneCommand) {

            //2. Não deve ser permitido inserir uma operação de alteração de estado no futuro.
            if (sceneCommand.OperationHour > DateTime.Now && !this.IsUndoOperation(this.State, sceneCommand.NextState)) {
                this.Erro = ErroEnum.FutureAlterNotAllowed;
            }                
            //3. Não deve ser permitido desfazer uma operação de alteração de estado que foi realizada há mais de 5 minutos. (opcional)
            else if (sceneCommand.OperationHour.Subtract(this.OperationHour).TotalMinutes > 5 && this.IsUndoOperation(this.State, sceneCommand.NextState)) {
                this.Erro = ErroEnum.CantUndoOperation;
            } 
            else if (!System.Enum.IsDefined(typeof(StateEnum), sceneCommand.NextState))
                this.Erro = ErroEnum.InvalidState;

            if (this.State == StateEnum.Pendente && sceneCommand.NextState == StateEnum.Gravada) {
                this.Erro = ErroEnum.OpNotAllowed;
            } else if (this.State == StateEnum.Pendurada && sceneCommand.NextState != StateEnum.Preparada) {
                this.Erro = ErroEnum.OpNotAllowed;
            } else if (this.State == StateEnum.Gravada && sceneCommand.NextState != StateEnum.Preparada) {
                this.Erro = ErroEnum.OpNotAllowed;
            }        

            if (this.Erro == ErroEnum.NoError) {
                this.State = sceneCommand.NextState;
                this.OperationHour = sceneCommand.OperationHour;
            }            
        }
        private bool IsUndoOperation(StateEnum current, StateEnum next) {
            return current != StateEnum.Pendente && (next == StateEnum.Pendente || next == StateEnum.Preparada);
        }
    }
}

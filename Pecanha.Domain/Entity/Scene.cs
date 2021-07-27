using Pecanha.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Pecanha.Domain.Entity {
    public class Scene : To {
        public string Name { get; private set; }
        public StateEnum State { get; private set; }
        public DateTime OperationHour { get; set; }
        public ICollection<RecordHistory> RecordHistories { get; set; }

        public Scene(string name) {
            Name = name;
            State = StateEnum.Pendente;
            OperationHour = DateTime.Now;
        }      

        public void UpdateState(StateEnum nextState, DateTime opHour) {
            this.State = nextState;
            this.OperationHour = opHour;
        }
    }
}

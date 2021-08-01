using Pecanha.Domain.Enum;
using System;

namespace Pecanha.Domain.Entity {
    public class RecordHistory  {

        public RecordHistory(int sceneId) {
            PreviousState = StateEnum.Pendente;
            CurrentState = StateEnum.Pendente;
            OperationHour = DateTime.Now;
            SceneId = sceneId;
        }

        public RecordHistory(int sceneId, StateEnum actualState, StateEnum nextState, DateTime opHour) {           
            PreviousState = actualState;
            CurrentState = nextState;
            OperationHour = opHour;
            SceneId = sceneId;
        }

        public int Id { get; set; }
        public DateTime OperationHour { get; private set; }
        public StateEnum PreviousState { get; private set; }
        public StateEnum CurrentState { get; private set; }
        public Scene Scene { get; private set; }
        public int SceneId { get; private set; }


        public void UpdateRecord(StateEnum nextState, DateTime opHour) {
            this.PreviousState = this.CurrentState;
            this.CurrentState = nextState;
            this.OperationHour = opHour;
        }
    }
}

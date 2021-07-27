using Pecanha.Domain.Enum;
using System;

namespace Pecanha.Domain.Entity {
    public class RecordHistory  {

        public RecordHistory(int sceneId) {
            PreviousState = StateEnum.Pendente;
            ActualState = StateEnum.Pendente;
            OperationHour = DateTime.Now;
            SceneId = sceneId;
        }

        public RecordHistory(int sceneId, StateEnum actualState, StateEnum nextState, DateTime opHour) {           
            PreviousState = actualState;
            ActualState = nextState;
            OperationHour = opHour;
            SceneId = sceneId;
        }

        public int Id { get; set; }
        public DateTime OperationHour { get; private set; }
        public StateEnum PreviousState { get; private set; }
        public StateEnum ActualState { get; private set; }
        public Scene Scene { get; private set; }
        public int SceneId { get; private set; }


        public void UpdateRecord(StateEnum nextState, DateTime opHour) {
            this.PreviousState = this.ActualState;
            this.ActualState = nextState;
            this.OperationHour = opHour;
        }
    }
}

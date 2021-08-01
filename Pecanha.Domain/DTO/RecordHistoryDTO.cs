using Pecanha.Domain.Entity;
using System;

namespace Pecanha.Domain.DTO {
    public class RecordHistortyDTO {
        public int SceneId { get; set; }
        public State ActualState { get; set; }
        public State PreviousState { get; set; }        
        public DateTime OperationDate { get; set; }

        public RecordHistortyDTO(RecordHistory history) {
            this.OperationDate = history.OperationHour;
            this.SceneId = history.SceneId;            

            this.PreviousState = new State() {
                StateDesc = history.PreviousState.ToString(),
                StateId = (int)history.PreviousState
            };

            this.ActualState = new State() {
                StateDesc = history.CurrentState.ToString(),
                StateId = (int)history.CurrentState
            };
        }

        public class State {
            public string StateDesc { get; set; }
            public int StateId { get; set; }
        }
    }    
}

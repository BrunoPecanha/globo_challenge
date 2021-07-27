using Pecanha.Domain.Entity;
using System;

namespace ScenesApi.DTO {
    public class RecordHistortyDTO {
        public int SceneId { get; set; }      
        public State PreviousState { get; set; }
        public State ActualState { get; set; }
        public DateTime OperationDate { get; set; }

        public RecordHistortyDTO(RecordHistory history) {
            this.OperationDate = history.OperationHour;
            this.SceneId = history.SceneId;            

            this.PreviousState = new State() {
                StateDesc = history.PreviousState.ToString(),
                StateId = (int)history.PreviousState
            };

            this.ActualState = new State() {
                StateDesc = history.ActualState.ToString(),
                StateId = (int)history.ActualState
            };
        }

        public class State {
            public string StateDesc { get; set; }
            public int StateId { get; set; }
        }
    }    
}

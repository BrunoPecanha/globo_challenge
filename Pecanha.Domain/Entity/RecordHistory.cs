using Pecanha.Domain.Enum;

namespace Pecanha.Domain.Entity {
    public class RecordHistory : To {
        public StateEnum PreviousState { get; set; }
        public StateEnum ActualState { get; set; }
        public Scene Scene { get; set; }
        public int SceneId { get; set; }
    }
}

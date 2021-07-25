using Pecanha.Domain.Enum;
using System.Collections.Generic;

namespace Pecanha.Domain.Entity {
    public class Scene : To {
        public string Name { get; private set; }
        public StateEnum State { get; private set; }
        public IList<RecordHistory> RecordHistory { get; private set; }

        public Scene(string name) {
            Name = name;
            State = StateEnum.Pendente;
            RecordHistory = new List<RecordHistory>();
        }
    }
}

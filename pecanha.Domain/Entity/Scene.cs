using Pecanha.Domain.Enum;

namespace Pecanha.Domain.Entity {
    public class Scene : To {
        public string Nome { get; set; }
        public StateEnum Estado { get; set; }
    }
}

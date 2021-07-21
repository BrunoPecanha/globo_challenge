using Pecanha.Domain.Enum;

namespace Pecanha.Domain.Entity {
    public class Cena : To {
        public string Nome { get; set; }
        public EstadoEnum Estado { get; set; }
    }
}

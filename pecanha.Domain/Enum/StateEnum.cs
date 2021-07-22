using System.ComponentModel;

namespace Pecanha.Domain.Enum {
    public enum StateEnum {
        [Description("Pendente")]
        Pendente = 1,
        [Description("Preparada")]
        Preparada = 2,
        [Description("Gravada")]
        Gravada = 3,
        [Description("Pendurada")]
        Pendurada = 4
    }
}

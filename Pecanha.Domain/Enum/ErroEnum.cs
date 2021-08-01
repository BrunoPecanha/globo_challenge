using System.ComponentModel;

namespace Pecanha.Domain.Enum {
    public enum ErroEnum {
        [Description("No Error")]
        NoError = 0,
        [Description("Future Alter Not Allowed")]
        FutureAlterNotAllowed = 1,
        [Description("Operation Not Allowed")]
        OpNotAllowed = 2,
        [Description("Can't Undo Operation")]
        CantUndoOperation = 3,
        [Description("Invalid State")]
        InvalidState = 4
    }
}

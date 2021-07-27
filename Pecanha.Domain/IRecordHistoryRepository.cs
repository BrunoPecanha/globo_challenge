using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;

namespace Pecanha.Domain {
    public interface IRecordHistoryRepository : IRepositoryBase<RecordHistory> {
        CommandResult GetRecordHistoryById(int id);
    }
}



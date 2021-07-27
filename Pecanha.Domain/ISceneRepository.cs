using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;

namespace Pecanha.Domain {
    public interface ISceneRepository : IRepositoryBase<Scene> {
        CommandResult GetById(int id);
        CommandResult GetAll(int? page, int? qtd);
        CommandResult GetRecordHistoryById(int id);
    }
}

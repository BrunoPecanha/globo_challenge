using Microsoft.EntityFrameworkCore;
using Pecanha.Domain.Entity;

namespace Pecanha.Repository.Context {
    public interface ISceneContext {
        DbSet<Scene> Scene { get; }
        DbSet<RecordHistory> RecordHistory { get; }
        int SaveChanges();
    }
}

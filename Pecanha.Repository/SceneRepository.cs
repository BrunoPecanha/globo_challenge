using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;
using Pecanha.Repository.Context;

namespace Pecanha.Repository {
    public class SceneRepository : RepositoryBase<Scene>, ISceneRepository {
        private readonly ISceneContext _dbContext;
        public SceneRepository(ISceneContext dbContext) {
            _dbContext = dbContext;
        }

        public void ChangeState(SceneCreateCommand sceneCommand) {
            throw new System.NotImplementedException();
        }

        public void Create(SceneCreateCommand sceneCommand) {
            _dbContext.Scene.Add(sceneCommand.CreteScene(sceneCommand.Name));           
        }
    }
}

using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;

namespace Pecanha.Service {
    public class SceneService : ServiceBase<Scene>, ISceneService {
        private readonly ISceneRepository _repository;
        public SceneService(ISceneRepository repository)
            : base(repository) {
            _repository = repository;
        }

        public void ChangeState(SceneCommand sceneCommand) {
            throw new System.NotImplementedException();
        }

        public void Create(SceneCommand sceneCommand) {
            _repository.Add(sceneCommand.CreteScene(sceneCommand.SceneName));           
        }
    }
}

using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;

namespace Pecanha.Domain {
    public interface ISceneService :  IServiceBase<Scene> {
        public Scene Create(SceneCreateCommand sceneCommand);
        public bool ChangeState(SceneUpdateCommand sceneCommand);
    }
}

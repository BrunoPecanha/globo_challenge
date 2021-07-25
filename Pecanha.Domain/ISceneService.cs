using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;

namespace Pecanha.Domain {
    public interface ISceneService :  IServiceBase<Scene> {
        public void Create(SceneCommand sceneCommand);
        public void ChangeState(SceneCommand sceneCommand);
    }
}

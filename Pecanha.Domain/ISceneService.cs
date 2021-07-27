using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;

namespace Pecanha.Domain {
    public interface ISceneService :  IServiceBase<Scene> {
        public CommandResult Create(SceneCreateCommand sceneCommand);
        public CommandResult ChangeState(SceneUpdateCommand sceneCommand);
    }
}

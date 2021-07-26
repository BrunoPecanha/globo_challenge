using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;
using System;

namespace Pecanha.Service {
    public class SceneService : ServiceBase<Scene>, ISceneService {
        private readonly ISceneRepository _repository;
        public SceneService(ISceneRepository repository)
            : base(repository) {
            _repository = repository;
        }

        public bool ChangeState(SceneUpdateCommand sceneCommand) {
            throw new System.NotImplementedException();
        }

        public Scene Create(SceneCreateCommand sceneCommand) {
            var scene = sceneCommand.CreteScene(sceneCommand.Name);
            if (scene is null)
                throw new Exception("Não é permitido nome vazio.");

            _repository.Add(scene);
            return scene;
        }
    }
}

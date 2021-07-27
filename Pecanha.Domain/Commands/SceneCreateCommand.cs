using Pecanha.Domain.Entity;

namespace Pecanha.Domain.Commands {

    /// <summary>
    /// Classe modelo para entrada de novas cenas. 
    /// </summary>
    public class SceneCreateCommand {
        public string Name { get; set; }

        public Scene ToEntity(string name) {
            Scene scene = null;
            if (!string.IsNullOrEmpty(name)) 
                scene = new Scene(name);
            
            return scene;
        }
    }
}

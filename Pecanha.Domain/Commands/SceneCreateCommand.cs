using Pecanha.Domain.Entity;

namespace Pecanha.Domain.Commands {

    /// <summary>
    /// Classe modelo para entrada de novas cenas. 
    /// Caso precise colocar mais props no cadastro, adicionar aqui que será replicado no swagger
    /// </summary>
    public class SceneCreateCommand {
        public string Name { get; set; }

        public Scene CreteScene(string name) {
            Scene scene = null;
            if (!string.IsNullOrEmpty(name)) 
                scene = new Scene(name);
            
            return scene;
        }
    }
}

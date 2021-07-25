using Pecanha.Domain.Entity;

namespace Pecanha.Domain.Commands {
    
    /// <summary>
    /// Classe modelo para entrada de novas cenas. Caso precise colocar mais props no cadastro, adicionar aqui que será replicado no swagger
    /// </summary>
    public class SceneCommand {
        public string SceneName { get; set; }      
        
        public Scene CreteScene(string name) {
            return new Scene(name);
        }
    }
}

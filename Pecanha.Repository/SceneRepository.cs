using Microsoft.EntityFrameworkCore;
using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;
using Pecanha.Repository.Context;
using System;
using System.Linq;

namespace Pecanha.Repository {
    public class SceneRepository : RepositoryBase<Scene>, ISceneRepository {
        private readonly ISceneContext _dbContext;
        private string _msgNotFoundById = "Não foi encontrado registro com id: {0}";
        private const string _msgNotFound = "Não foram encontrados registros";

        public SceneRepository(ISceneContext dbContext) {
            _dbContext = dbContext;
        }

        //F2. Listar cenas com seus estados atuais;
        public CommandResult GetAll(int? page, int? qtd) {
            try {
                var scene = _dbContext.Scene.Skip(((int)page-1) * (int)qtd)
                                            .Take((int)qtd)
                                            .OrderByDescending(x => x.RegisteringDate)
                                            .AsNoTracking()
                                            .ToArray();
                if (scene.Length < 1) {
                    return new CommandResult(true, false, _msgNotFound, null);
                }

                return new CommandResult(true, false, string.Empty, scene);

            } catch (Exception ex) {
                return new CommandResult(false, true, ex.Message, null);
            }
        }

        //F3. Obter dado de uma cena específica.
        public CommandResult GetById(int id) {
            try {
                var scene = _dbContext.Scene
                                      .AsNoTracking()
                                      .FirstOrDefault(x => x.Id == id);

                if (scene is null) {
                    return new CommandResult(false, false, string.Format(_msgNotFoundById, id), null);
                }

                return new CommandResult(true, false, string.Empty, scene);

            } catch (Exception ex) {
                return new CommandResult(false, true, ex.Message, null);
            }
        }
       
    }
}

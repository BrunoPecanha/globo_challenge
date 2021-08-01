using Microsoft.EntityFrameworkCore;
using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Domain.DTO;
using Pecanha.Domain.Entity;
using Pecanha.Repository.Context;
using System;
using System.Linq;

namespace Pecanha.Repository {
    public class RecordHistoryRepository : RepositoryBase<RecordHistory>, IRecordHistoryRepository {
        private readonly ISceneContext _dbContext;
        private const string _msgNoChanges = "Não foram encontrados registros de alteração de estado desta cena";

        public RecordHistoryRepository(ISceneContext dbContext) {
            _dbContext = dbContext;
        }

        // F4. Implementar mecanismo de auditoria que me permita saber quais operações foram realizadas independente do estado atual;
        public CommandResult GetRecordHistoryById(int id) {
            try {
                var history = _dbContext.RecordHistory
                                        .Where(x => x.SceneId == id)
                                        .OrderByDescending(x => x.OperationHour)
                                        .AsNoTracking()
                                        .Select(x => new RecordHistortyDTO(x))
                                        .ToList();

                if (history.Count() < 1) {
                    return new CommandResult(true, false, _msgNoChanges, null);
                }

                return new CommandResult(true, false, string.Empty, history);

            } catch (Exception ex) {
                return new CommandResult(false, true, ex.Message, null);
            }
        }
    }
}

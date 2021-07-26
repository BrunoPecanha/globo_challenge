using Microsoft.EntityFrameworkCore;
using Pecanha.Domain;
using Pecanha.Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Pecanha.Repository {

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class {
        // Cria uma instância de acesso ao BD.
        protected SceneContext Db = new SceneContext(new DbContextOptions<SceneContext>(), null);

        public void Add(T obj) {
            Db.Set<T>().Add(obj);
            Db.SaveChanges();
        }

        public void Dispose() {
            Db.Dispose();
        }

        public IList<T> GetAll() {
            return Db.Set<T>().ToArray();
        }

        public T GetById(int id) {
            return Db.Set<T>().Find(id);
        }

        public void Remove(T obj) {
            Db.Set<T>().Remove(obj);
            Db.SaveChanges();
        }

        public void Update(T obj) {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}


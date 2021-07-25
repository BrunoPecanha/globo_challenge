using Pecanha.Domain;
using System;
using System.Collections.Generic;

namespace Pecanha.Service {
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class {

        private readonly IRepositoryBase<TEntity> _repository;

        /// <summary>
        /// IoC
        /// </summary>
        /// <param name="repository">Instância de repositorio.</param>
        public ServiceBase(IRepositoryBase<TEntity> repository) {
            _repository = repository;
        }

        /// <summary>
        /// Adiciona um objeto T.
        /// </summary>
        /// <param name="obj">Objeto.</param>
        public void Add(TEntity obj) {
            _repository.Add(obj);
        }

        /// <summary>
        /// Desaloca recursos não recolhidos pelo garbage collector.
        /// </summary>
        public void Dispose() {
            _repository.Dispose();
        }
        /// <summary>
        /// Recupera uma lista de um obj.
        /// </summary>
        public IList<TEntity> GetAll() {
            return _repository.GetAll();
        }
        /// <summary>
        /// Recupera um determinado objeto.
        /// </summary>
        /// <param name="obj">Objeto.</param>
        public TEntity GetById(int id) {
            return _repository.GetById(id);
        }
        /// <summary>
        /// Remove um objeto T.
        /// </summary>
        /// <param name="obj">Objeto.</param>
        public void Remove(TEntity obj) {
            _repository.Remove(obj);
        }

        /// <summary>
        /// Atualiza um objeto T.
        /// </summary>
        /// <param name="obj">Objeto.</param>
        public void Update(TEntity obj) {
            _repository.Update(obj);
        }
    }
}

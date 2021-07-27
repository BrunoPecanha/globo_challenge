using Pecanha.Domain;
using System;

namespace Pecanha.Service {
    public class ServiceBase<T> : IDisposable, IServiceBase<T> where T : class {

        private readonly IRepositoryBase<T> _repository;

        /// <summary>
        /// IoC
        /// </summary>
        /// <param name="repository">Instância de repositorio.</param>
        public ServiceBase(IRepositoryBase<T> repository) {
            _repository = repository;
        }

        /// <summary>
        /// Adiciona um objeto T.
        /// </summary>
        /// <param name="obj">Objeto.</param>
        public void Add(T obj) {
            _repository.Add(obj);
        }

        /// <summary>
        /// Desaloca recursos não recolhidos pelo garbage collector.
        /// </summary>
        public void Dispose() {
            _repository.Dispose();
        }       

        /// <summary>
        /// Atualiza um objeto T.
        /// </summary>
        /// <param name="obj">Objeto.</param>
        public void Update(T obj) {
            _repository.Update(obj);
        }
    }
}

using System.Collections.Generic;

namespace Pecanha.Domain {
    public interface IRepositoryBase<T> where T : class {
        /// <summary>
        /// Grava o objeto T.
        /// </summary>
        /// <param name="obj">Objeto que será persistido.</param>
        void Add(T obj);
        /// <summary>
        /// Retorna um registro de acordo com o Id passado.
        /// </summary>
        /// <param name="id">Id do registro que será procurado no BD.</param>
        T GetById(int id);
        /// <summary>
        /// Retorna todos os elementos do tipo T.
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();
        /// <summary>
        /// Atualiza um registro T já existente no BD.
        /// </summary>
        /// <param name="obj">Objeto que sera atualizado.</param>
        void Update(T obj);
        /// <summary>
        /// Remove um registro do BD
        /// </summary>
        /// <param name="obj">Objeto que será removido.</param>
        void Remove(T obj);
        /// <summary>
        /// Liberar recursos não gerenciados.
        /// </summary>
        void Dispose();
    }
}

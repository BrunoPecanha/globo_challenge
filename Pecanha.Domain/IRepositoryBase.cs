namespace Pecanha.Domain {
    public interface IRepositoryBase<T> where T : class {
        /// <summary>
        /// Grava o objeto T.
        /// </summary>
        /// <param name="obj">Objeto que será persistido.</param>
        void Add(T obj);
        /// <summary>
        /// Atualiza um registro T já existente no BD.
        /// </summary>
        /// <param name="obj">Objeto que sera atualizado.</param>
        void Update(T obj);

        /// <summary>
        /// Liberar recursos não gerenciados.
        /// </summary>
        void Dispose();
    }
}

using System;

namespace Pecanha.Domain.Entity {
    public class To {
        /// <summary>
        /// Id do registro
        /// </summary>       
        public int Id { get; private set; }
        /// <summary>
        /// Data de cadastro do registro.
        /// </summary>
        public DateTime RegisteringDate { get; private set; }
        /// <summary>
        /// Última atualização do registro.
        /// </summary>
        public DateTime LastUpdate { get; private set; }
    }
}

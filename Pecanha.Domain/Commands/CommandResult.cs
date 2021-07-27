namespace Pecanha.Domain.Commands {
    public class CommandResult {

        public CommandResult(bool valid, bool error, string messege, object log) {
            this.Valid = valid;
            this.Error = error;
            this.Message = messege;
            this.Log = log;
        }
        /// Indica se a operação foi validada
        /// </summary>

        public bool Valid { get; set; }
        /// <summary>
        /// Indica se houve erro de processamento
        /// </summary>

        public bool Error { get; set; }
        /// <summary>
        /// Messagem Padrão de saída
        /// </summary>

        public string Message { get; set; }
        /// <summary>
        /// Objeto de saída
        /// </summary>

        public object Log { get; set; }

    }
}

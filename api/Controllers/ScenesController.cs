using Microsoft.AspNetCore.Mvc;
using Pecanha.Domain;
using Pecanha.Domain.Commands;
using System.Threading.Tasks;

namespace api.Controllers {
    [Route("api")]
    public class ScenesController : Controller {
        private readonly ISceneService _service;
        private readonly ISceneRepository _repository;

        public ScenesController(ISceneService service, ISceneRepository repository) {
            _repository = repository;
            _service = service;
        }

        /// <summary>
        /// Endpoint para recuperação das cenas cadastradas
        /// </summary>
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id) {
            return View();
        }

        /// <summary>
        /// Endpoint para recuperação das cenas cadastradas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get() {
            var listaScenes =  _repository.GetAll();
            return Ok(listaScenes);
        }

        /// <summary>
        /// Endpoint para criação do pedido de cena
        /// </summary>
        [HttpPost]        
        public async Task<IActionResult> Add([FromBody] SceneCommand command) {
            _service.Create(command);
            return Ok();
        }

        /// <summary>
        /// Endpoint para atualização do estado da cena
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery]int id, int estado) {
            return View();
        }
    }
}

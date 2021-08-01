using Microsoft.AspNetCore.Mvc;
using Pecanha.Domain;
using Pecanha.Domain.Commands;

namespace api.Controllers {
    [Route("api")]
    //F1.Controlar estados de cenas;
    public class ScenesController : Controller {
        private readonly ISceneService _service;
        private readonly ISceneRepository _sceneRepository;
        private readonly IRecordHistoryRepository _recordHistoryRepository;

        public ScenesController(ISceneService service, ISceneRepository repository, IRecordHistoryRepository recordHistoryRepository) {
            _sceneRepository = repository;
            _service = service;
            _recordHistoryRepository = recordHistoryRepository;
        }

        /// <summary>
        /// Endpoint para recuperação do historico de alterações nas cenas
        /// </summary>
        [HttpGet("history")]
        public IActionResult GetHistory([FromQuery] int id) {         
            var ret = _recordHistoryRepository.GetRecordHistoryById(id);

            if (ret.Valid && !ret.Error && ret.Log != null)
                return Ok(ret);
            else if (ret.Valid && !ret.Error)
                return NoContent();
            else
                return BadRequest(ret);
        }

        /// <summary>
        /// Endpoint para recuperação de cena por id
        /// </summary>
        [HttpGet("id")]
        public IActionResult Get([FromQuery] int id) {
            var ret = _sceneRepository.GetById(id);
            if (ret.Valid && !ret.Error && ret.Log != null)
                return Ok(ret);
            if (ret.Valid && !ret.Error)
                return NoContent();
            else
                return BadRequest(ret);
        }

        /// <summary>
        /// Endpoint para recuperação das cenas cadastradas
        /// </summary>
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, int qtt = 10) {
            var ret = _sceneRepository.GetAll(page, qtt);
            if (ret.Valid && !ret.Error && ret.Log != null)
                return Ok(ret);
            else if (ret.Valid && !ret.Error)
                return NoContent();
            else
                return BadRequest(ret);
        }

        /// <summary>
        /// Endpoint para criação do pedido de cena
        /// </summary>
        [HttpPost]
        public IActionResult Add([FromBody] SceneCreateCommand command) {
            var ret = _service.Create(command);
            if (ret.Valid)
                return Created(string.Empty, ret);
            else if (!ret.Valid && !ret.Error)
                return StatusCode(422, ret);
            else
                return BadRequest(ret);
        }

        /// <summary>
        /// Endpoint para atualização do estado da cena
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromBody] SceneUpdateCommand command) {
            var ret = _service.ChangeState(command);
            if (ret.Valid)
                return Ok();
            else if (!ret.Valid && !ret.Error)
                return StatusCode(422, ret);
            else
                return BadRequest(ret);
        }
    }
}

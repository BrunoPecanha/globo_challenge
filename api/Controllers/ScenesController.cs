using Microsoft.AspNetCore.Mvc;
using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Domain.Entity;
using ScenesApi.DTO;
using System.Collections.Generic;

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
        /// Endpoint para recuperação do historico de alterações nas cenas
        /// </summary>
        [HttpGet("history")]
        public IActionResult GetHistory([FromQuery] int id) {
            List<RecordHistortyDTO> recordList = new List<RecordHistortyDTO>();
            var ret = _repository.GetRecordHistoryById(id);

            if (ret.Log != null)
                (ret.Log as List<RecordHistory>).ForEach(x => recordList.Add(new RecordHistortyDTO(x)));

            if (ret.Valid && ret.Log != null)
                return Ok(recordList);
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
            var ret = _repository.GetById(id);
            if (ret.Valid && ret.Log != null)
                return Ok(ret);
            else if (ret.Valid && !ret.Error)
                return NoContent();
            else
                return BadRequest(ret);
        }

        /// <summary>
        /// Endpoint para recuperação das cenas cadastradas
        /// </summary>
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 0, int qtt = 10) {
            var ret = _repository.GetAll(page, qtt);
            if (ret.Valid && ret.Log != null)
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
            else if (!ret.Valid && ret.Error)
                return StatusCode(422, ret);
            else
                return BadRequest(ret);
        }
    }
}

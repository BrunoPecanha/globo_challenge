using Microsoft.AspNetCore.Mvc;
using Pecanha.Domain;
using Pecanha.Domain.Commands;
using Pecanha.Service.Containers;
using System;

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
        /// Endpoint para recuperação das cenas cadastradas por id
        /// </summary>
        [HttpGet("id")]
        public IActionResult Get([FromHeader] int id) {
            return Ok(_repository.GetById(id));
        }

        /// <summary>
        /// Endpoint para recuperação das cenas cadastradas
        /// </summary>
        [HttpGet]
        public IActionResult Get() {
            return Ok(_repository.GetAll());
        }

        /// <summary>
        /// Endpoint para criação do pedido de cena
        /// </summary>
        [HttpPost]
        public IActionResult Add([FromBody] SceneCreateCommand command) {
            try {
                var scene = _service.Create(command);
                return Ok(
                   new ContainerResult() {
                       Id = scene.Id,
                       Valid = true,
                       Log = scene
                   }
                );
            } catch (Exception ex) {
                return BadRequest(
                    new ContainerResult() {
                        Valid = false,
                        Message = ex.Message
                    }
                );
            }
        }

        /// <summary>
        /// Endpoint para atualização do estado da cena
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromQuery] int id, int estado, DateTime operationHour) {
            return null;
        }
    }
}

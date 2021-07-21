using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers {
    [Route("api")]
    public class CenaController : Controller {
        /// <summary>
        /// Endpoint para criação do pedido de cena
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get() {
            return View();
        }

        /// <summary>
        /// Endpoint para criação do pedido de cena
        /// </summary>
        [HttpPost]        
        public async Task<IActionResult> Add() {
            return View();
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

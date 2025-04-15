using ConCorrenteDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConCorrente.WebAPI.Controllers {
    public class TestController : Controller {
        private readonly IContaCorrenteRepository _repo;

        public TestController(IContaCorrenteRepository repo) {
            _repo = repo;
        }

        [HttpGet("contas")]
        public async Task<IActionResult> GetContas() {
            var contas = await _repo.GetContaCorrentes();
            return Ok(contas);
        }
    }
}

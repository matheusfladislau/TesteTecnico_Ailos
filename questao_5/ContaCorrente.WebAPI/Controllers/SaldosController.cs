using ConCorrente.Application.Saldos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConCorrente.WebAPI.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class SaldosController : ControllerBase {
    private readonly IMediator _mediator;
    public SaldosController(IMediator mediator) {
        this._mediator = mediator;
    }

    [HttpGet("GetSaldoContaCorrente/{idContaCorrente}")]
    public async Task<IActionResult> GetSaldoContaCorrente(string idContaCorrente) {
        var query = new GetSaldoContaCorrenteQuery(idContaCorrente);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

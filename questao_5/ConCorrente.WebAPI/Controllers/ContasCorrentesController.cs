using ConCorrente.Application.ContasCorrentes.Queries;
using ConCorrente.Application.Saldos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConCorrente.WebAPI.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class ContasCorrentesController : ControllerBase {
    private readonly IMediator _mediator;
    public ContasCorrentesController(IMediator mediator) {
        this._mediator = mediator;
    }
    
    [HttpGet("GetContasCorrentes")]
    public async Task<IActionResult> GetContasCorrentes() {
        var query = new GetContasCorrentesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetSaldoContaCorrente/{idContaCorrente}")]
    public async Task<IActionResult> GetSaldoContaCorrente(string idContaCorrente) {
        var query = new GetSaldoContaCorrenteQuery(idContaCorrente);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

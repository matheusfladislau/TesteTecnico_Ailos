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

    /// <summary>
    /// Retorna a lista de todas as contas correntes cadastradas.
    /// </summary>
    /// <returns>
    /// Retorna:
    /// - 200 OK: Uma lista de contas correntes.
    /// - 400 Bad Request: Dados inválidos ou inconsistentes.
    /// - 500 Internal Server Error: Erro inesperado no processamento.
    /// </returns>
    /// <response code="200">Uma lista de contas correntes.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="500">Erro interno ao processar a requisição.</response>
    [HttpGet("GetContasCorrentes")]
    public async Task<IActionResult> GetContasCorrentes() {
        var query = new GetContasCorrentesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Retorna o saldo da conta corrente informada pelo ID.
    /// </summary>
    /// <param name="idContaCorrente">ID da conta corrente.</param>
    /// <returns>
    /// Retorna:
    /// - 200 OK: Saldo da conta corrente.
    /// - 400 Bad Request: Dados inválidos ou inconsistentes.
    /// - 500 Internal Server Error: Erro inesperado no processamento.
    /// </returns>
    /// <response code="200">Saldo da conta corrente.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="500">Erro interno ao processar a requisição.</response>
    [HttpGet("GetSaldoContaCorrente/{idContaCorrente}")]
    public async Task<IActionResult> GetSaldoContaCorrente(string idContaCorrente) {
        var query = new GetSaldoContaCorrenteQuery(idContaCorrente);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

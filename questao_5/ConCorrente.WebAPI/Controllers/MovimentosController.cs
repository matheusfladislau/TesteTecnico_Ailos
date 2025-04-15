using ConCorrente.Application.Movimentos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConCorrente.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentosController : ControllerBase {

    private readonly IMediator _mediator;
    public MovimentosController(IMediator mediator) {
        this._mediator = mediator;
    }

    /// <summary>
    /// Adiciona um novo movimento financeiro (crédito ou débito) a uma conta corrente.
    /// </summary>
    /// <param name="command">Dados do movimento a ser criado.</param>
    /// <returns>
    /// Retorna:
    /// - 200 OK: Movimento registrado com sucesso.
    /// - 400 Bad Request: Dados inválidos ou inconsistentes.
    /// - 500 Internal Server Error: Erro inesperado no processamento.
    /// </returns>
    /// <response code="200">Movimento registrado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="500">Erro interno ao processar a requisição.</response>
    [HttpPost]
    public async Task<IActionResult> AddMovimento([FromBody] MovimentoCreateCommand command) {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

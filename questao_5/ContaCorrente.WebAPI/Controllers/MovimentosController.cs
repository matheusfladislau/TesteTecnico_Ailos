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

    [HttpPost]
    public async Task<IActionResult> AddMovimento([FromBody] MovimentoCreateCommand command) {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

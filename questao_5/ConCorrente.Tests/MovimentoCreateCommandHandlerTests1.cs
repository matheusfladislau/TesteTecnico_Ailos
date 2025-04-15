using ConCorrente.Application.Movimentos.Commands;
using ConCorrente.Application.Movimentos.Handlers;
using ConCorrente.Domain.Entities;
using ConCorrente.Domain.Interfaces;
using ConCorrenteDomain.Entities;
using ConCorrenteDomain.Interfaces;
using ConCorrenteDomain.Validation;
using FluentAssertions;
using NSubstitute;
using System.Text.Json;

namespace ConCorrente.Tests; 
public class MovimentoCreateCommandHandlerTests1 {
    private readonly IMovimentoRepository _movimentoRepo = Substitute.For<IMovimentoRepository>();
    private readonly IContaCorrenteRepository _contaRepo = Substitute.For<IContaCorrenteRepository>();
    private readonly IIdempotenciaRepository _idempotenciaRepo = Substitute.For<IIdempotenciaRepository>();

    [Fact]
    public async Task Handle_ShouldCreateMovimento_ReturnResult() {
        var handler = new MovimentoCreateCommandHandler(_movimentoRepo, _contaRepo, _idempotenciaRepo);
        var command = new MovimentoCreateCommand {
            IdContaCorrente = "123",
            TipoMovimento = "C",
            Valor = 100,
            IdRequisicao = "abc"
        };

        _idempotenciaRepo.GetIdempotenciaAsync(command.IdRequisicao).Returns((Idempotencia)null);
        _contaRepo.GetById(command.IdContaCorrente).Returns(new ContaCorrente(123, "Teste", true));
        _movimentoRepo.AddAsync(Arg.Any<Movimento>()).Returns("mov123");

        var result = await handler.Handle(command, default);

        var expectedJson = JsonSerializer.Serialize(new { movimento = "mov123" });
        result.Should().Be(expectedJson);
        await _idempotenciaRepo.Received().AddAsync(Arg.Is<Idempotencia>(i => i.Chave_Idempotencia == "abc"));
    }

    [Fact]
    public async Task Handle_ShouldThrowException_ContaNotFound() {
        var handler = new MovimentoCreateCommandHandler(_movimentoRepo, _contaRepo, _idempotenciaRepo);
        var command = new MovimentoCreateCommand {
            IdContaCorrente = "inexistente",
            TipoMovimento = "C",
            Valor = 100,
            IdRequisicao = "req-1"
        };

        _idempotenciaRepo.GetIdempotenciaAsync(command.IdRequisicao).Returns((Idempotencia)null);
        _contaRepo.GetById(command.IdContaCorrente).Returns((ContaCorrente)null);

        Func<Task> act = async () => await handler.Handle(command, default);

        var exception = await act.Should()
            .ThrowAsync<MovimentoEntityValidation>()
            .WithMessage("Apenas contas cadastradas podem receber movimentação.");

        await _idempotenciaRepo.Received().AddAsync(Arg.Is<Idempotencia>(x =>
            x.Chave_Idempotencia == "req-1" &&
            x.Resultado.Contains("INVALID_ACCOUNT")
        ));
    }

    [Fact]
    public async Task Handle_ShouldThrowException_ContaIsInactive() {
        var handler = new MovimentoCreateCommandHandler(_movimentoRepo, _contaRepo, _idempotenciaRepo);
        var command = new MovimentoCreateCommand {
            IdContaCorrente = "123",
            TipoMovimento = "D",
            Valor = 50,
            IdRequisicao = "req-2"
        };

        var contaInativa = new ContaCorrente("123", 456, "Conta Inativa", false);
        _idempotenciaRepo.GetIdempotenciaAsync(command.IdRequisicao).Returns((Idempotencia)null);
        _contaRepo.GetById(command.IdContaCorrente).Returns(contaInativa);

        Func<Task> act = async () => await handler.Handle(command, default);

        var exception = await act.Should()
            .ThrowAsync<MovimentoEntityValidation>()
            .WithMessage("Apenas contas ativas podem receber movimentação.");

        await _idempotenciaRepo.Received().AddAsync(Arg.Is<Idempotencia>(x =>
            x.Chave_Idempotencia == "req-2" &&
            x.Resultado.Contains("INACTIVE_ACCOUNT")
        ));
    }

    [Fact]
    public async Task Handle_ShouldReturnIdempotent_IfExists() {
        var handler = new MovimentoCreateCommandHandler(_movimentoRepo, _contaRepo, _idempotenciaRepo);
        var command = new MovimentoCreateCommand {
            IdContaCorrente = "456",
            TipoMovimento = "C",
            Valor = 200,
            IdRequisicao = "req-3"
        };

        var resultadoAnterior = JsonSerializer.Serialize(new { movimento = "movimentacao-antiga" });
        var idempotente = new Idempotencia("req-3", JsonSerializer.Serialize(command), resultadoAnterior);

        _idempotenciaRepo.GetIdempotenciaAsync(command.IdRequisicao).Returns(idempotente);

        var result = await handler.Handle(command, default);

        result.Should().Be(resultadoAnterior);

        await _movimentoRepo.DidNotReceiveWithAnyArgs().AddAsync(default);
        await _contaRepo.DidNotReceiveWithAnyArgs().GetById(default);
    }
}

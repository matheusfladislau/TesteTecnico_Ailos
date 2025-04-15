using ConCorrente.Application.Saldos.Queries;
using ConCorrente.Domain.Exceptions;
using ConCorrenteDomain.Interfaces;
using ConCorrenteDomain.Validation;
using MediatR;
using System.Text.Json;

namespace ConCorrente.Application.Saldos.Handlers;
public class GetSaldoContaCorrenteQueryHandler : IRequestHandler<GetSaldoContaCorrenteQuery, string> {
    private readonly IContaCorrenteRepository _contaRepository;
    public GetSaldoContaCorrenteQueryHandler(IContaCorrenteRepository contaRepository) {
        this._contaRepository = contaRepository;
    }

    public async Task<string> Handle(GetSaldoContaCorrenteQuery request, CancellationToken cancellationToken) {
        var conta = await _contaRepository.GetById(request.IdContaCorrente);

        MovimentoEntityValidation.When(conta == null,
            "Apenas contas correntes cadastradas podem consultar o saldo.",
            ErrorType.INVALID_ACCOUNT);

        MovimentoEntityValidation.When(conta?.Ativo == false,
            "Apenas contas correntes ativas podem consultar o saldo.",
            ErrorType.INACTIVE_ACCOUNT);

        decimal qtSaldo = await _contaRepository.GetSaldoContaCorrenteAsync(request.IdContaCorrente);

        var resultJson = JsonSerializer.Serialize(new {
            idcontacorrente = conta.IdContaCorrente,
            nome = conta.Nome,
            dthrconsulta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
            saldo = qtSaldo
        });
        return resultJson;
    }
}


using ConCorrente.Application.Movimentos.Commands;
using ConCorrente.Domain.Entities;
using ConCorrente.Domain.Exceptions;
using ConCorrente.Domain.Interfaces;
using ConCorrenteDomain.Entities;
using ConCorrenteDomain.Interfaces;
using ConCorrenteDomain.Validation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ConCorrente.Application.Movimentos.Handlers;
public class MovimentoCreateCommandHandler : IRequestHandler<MovimentoCreateCommand, string> {
    private readonly IContaCorrenteRepository _contaRepository;
    private readonly IMovimentoRepository _movimentoRepository;
    private readonly IIdempotenciaRepository _idempotenciaRepository;
    public MovimentoCreateCommandHandler(IMovimentoRepository movimentoRepository,
        IContaCorrenteRepository contaRepository,
        IIdempotenciaRepository idempotenciaRepository) {
        this._contaRepository = contaRepository;
        this._movimentoRepository = movimentoRepository;
        this._idempotenciaRepository = idempotenciaRepository;
    }

    public async Task<string> Handle(MovimentoCreateCommand request, CancellationToken cancellationToken) {
        var existente = await _idempotenciaRepository.GetIdempotenciaAsync(request.IdRequisicao);
        if (existente != null) {
            return existente.Resultado;
        }

        try {
            var conta = await _contaRepository.GetById(request.IdContaCorrente);

            MovimentoEntityValidation.When(conta == null, 
                "Apenas contas cadastradas podem receber movimentação.", 
                ErrorType.INVALID_ACCOUNT);

            MovimentoEntityValidation.When(conta?.Ativo == false, 
                "Apenas contas ativas podem receber movimentação.", 
                ErrorType.INACTIVE_ACCOUNT);

            var movimento = new Movimento(DateTime.Now, request.TipoMovimento, 
                request.Valor, request.IdContaCorrente);
            var idMov = await _movimentoRepository.AddAsync(movimento);

            var resultJson = JsonSerializer.Serialize(new {
                movimento = idMov
            });

            var idempotencia = new Idempotencia(request.IdRequisicao,
                System.Text.Json.JsonSerializer.Serialize(request),
                resultJson);
            await _idempotenciaRepository.AddAsync(idempotencia);

            return resultJson;
        } catch (MovimentoEntityValidation ex) {
            var idempotenciaErro = new Idempotencia(request.IdRequisicao, System.Text.Json.JsonSerializer.Serialize(request),
               JsonSerializer.Serialize(new {
                   errorType = ex.ErrorType.ToString(),
                   message = ex.Message
               }));
            await _idempotenciaRepository.AddAsync(idempotenciaErro);

            throw;
        }
    }
}

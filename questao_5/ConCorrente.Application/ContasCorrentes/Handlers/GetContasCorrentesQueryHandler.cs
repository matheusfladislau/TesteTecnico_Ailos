using ConCorrente.Application.ContasCorrentes.Queries;
using ConCorrenteDomain.Entities;
using ConCorrenteDomain.Interfaces;
using MediatR;

namespace ConCorrente.Application.ContasCorrentes.Handlers;
public class GetContasCorrentesQueryHandler : IRequestHandler<GetContasCorrentesQuery, IEnumerable<ContaCorrente>> {
    private readonly IContaCorrenteRepository _repository;
    public GetContasCorrentesQueryHandler(IContaCorrenteRepository repository) {
        this._repository = repository;
    }

    public async Task<IEnumerable<ContaCorrente>> Handle(GetContasCorrentesQuery request, CancellationToken cancellationToken) {
        return await _repository.GetContasCorrentes();
    }
}

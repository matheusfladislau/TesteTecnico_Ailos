using ConCorrenteDomain.Entities;
using MediatR;

namespace ConCorrente.Application.ContasCorrentes.Queries; 
public class GetContasCorrentesQuery : IRequest<IEnumerable<ContaCorrente>> {
}

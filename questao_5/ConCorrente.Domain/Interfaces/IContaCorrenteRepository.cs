using ConCorrenteDomain.Entities;

namespace ConCorrenteDomain.Interfaces;
public interface IContaCorrenteRepository {
    Task<ContaCorrente> GetById(string id);
    Task<IEnumerable<ContaCorrente>> GetContasCorrentes();
    Task<decimal> GetSaldoContaCorrenteAsync(string idContaCorrente);
}

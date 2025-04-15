using ConCorrenteDomain.Entities;

namespace ConCorrenteDomain.Interfaces; 
public interface IMovimentoRepository {
    Task<Movimento> GetMovimentoAsync(string id);

    Task<string> AddAsync(Movimento movimento);
}

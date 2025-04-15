using ConCorrente.Domain.Entities;

namespace ConCorrente.Domain.Interfaces; 
public interface IIdempotenciaRepository {
    Task<Idempotencia> GetIdempotenciaAsync(string id);

    Task AddAsync(Idempotencia idempotencia);
    Task RemoveAsync(string id);
    Task UpdateAsync(Idempotencia idempotencia);
}

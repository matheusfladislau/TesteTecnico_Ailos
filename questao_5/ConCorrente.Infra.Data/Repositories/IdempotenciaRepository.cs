using ConCorrente.Domain.Entities;
using ConCorrente.Domain.Interfaces;
using ConCorrente.Infra.Data.Interfaces;
using Dapper;
using System.Net.Http.Headers;

namespace ConCorrente.Infra.Data.Repositories; 
public class IdempotenciaRepository : IIdempotenciaRepository {
    private readonly IDbConnectionFactory _connectionFactory;
    public IdempotenciaRepository(IDbConnectionFactory connectionFactory) {
        _connectionFactory = connectionFactory;
    }

    public async Task AddAsync(Idempotencia idempotencia) {
        const string sql = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado)" +
            "VALUES (@id, @requisicao, @resultado)";

        using (var connection = _connectionFactory.CreateConnection()) {
            await connection.ExecuteAsync(sql,
                new {
                    id = idempotencia.Chave_Idempotencia,
                    requisicao = idempotencia.Requisicao,
                    resultado = idempotencia.Resultado
                });
        }
    }

    public async Task<Idempotencia> GetIdempotenciaAsync(string id) {
        const string sql = "SELECT * FROM idempotencia WHERE chave_idempotencia = @id";

        using (var connection = _connectionFactory.CreateConnection()) {
            return await connection.QueryFirstOrDefaultAsync<Idempotencia>(sql, new { id });
        }
    }

    public async Task RemoveAsync(string id) {
        const string sql = "DELETE FROM idempotencia WHERE chave_idempotencia = @id";

        using (var connection = _connectionFactory.CreateConnection()) {
            await connection.ExecuteAsync(sql, new { id = id });
        }
    }

    public async Task UpdateAsync(Idempotencia idempotencia) {
        const string sql = @"UPDATE idempotencia 
                             SET requisicao = @requisicao, resultado = @resultado
                             WHERE chave_idempotencia = @id";

        using (var connection = _connectionFactory.CreateConnection()) {
            await connection.ExecuteAsync(sql,
                new {
                    id = idempotencia.Chave_Idempotencia,
                    requisicao = idempotencia.Requisicao,
                    resultado = idempotencia.Resultado
                });
        }
    }
}

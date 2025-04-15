using ConCorrente.Domain.Entities;
using ConCorrente.Infra.Data.Interfaces;
using ConCorrenteDomain.Entities;
using ConCorrenteDomain.Interfaces;
using Dapper;

namespace ConCorrente.Infra.Data.Repositories;
public class MovimentoRepository : IMovimentoRepository {
    private readonly IDbConnectionFactory _connectionFactory;
    public MovimentoRepository(IDbConnectionFactory connectionFactory) {
        this._connectionFactory = connectionFactory;
    }

    public async Task AddAsync(Movimento movimento) {
        var sql = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                    VALUES (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)";

        using (var connection = _connectionFactory.CreateConnection()) {
            await connection.ExecuteAsync(sql, new {
                idmovimento = movimento.IdMovimento.ToString(),
                idcontacorrente = movimento.IdContaCorrente.ToString(),
                datamovimento = movimento.DataMovimento.ToString("dd/mm/YYYY"),
                tipomovimento = movimento.TipoMovimento,
                valor = movimento.Valor
            });
        }
    }

    public async Task<Movimento> GetMovimentoAsync(string id) {
        const string sql = "SELECT * FROM movimento WHERE idmovimento = @id";

        using (var connection = _connectionFactory.CreateConnection()) {
            return await connection.QueryFirstOrDefaultAsync<Movimento>(sql, new { id = id });
        }
    }
}

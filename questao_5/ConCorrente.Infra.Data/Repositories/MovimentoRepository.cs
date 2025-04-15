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

    public async Task<string> AddAsync(Movimento movimento) {
        var id = Guid.NewGuid();

        var sql = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                    VALUES (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)";

        using (var connection = _connectionFactory.CreateConnection()) {
            await connection.ExecuteAsync(sql, new {
                idmovimento = id.ToString(),
                idcontacorrente = movimento.IdContaCorrente,
                datamovimento = movimento.DataMovimento.ToString("dd/MM/yyyy"),
                tipomovimento = movimento.TipoMovimento,
                valor = movimento.Valor
            });
        }
        return id.ToString();
    }

    public async Task<Movimento> GetMovimentoAsync(string id) {
        const string sql = "SELECT * FROM movimento WHERE idmovimento = @id";

        using (var connection = _connectionFactory.CreateConnection()) {
            return await connection.QueryFirstOrDefaultAsync<Movimento>(sql, new { id = id });
        }
    }
}

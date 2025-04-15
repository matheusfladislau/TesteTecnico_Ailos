using ConCorrente.Infra.Data.Interfaces;
using ConCorrenteDomain.Entities;
using ConCorrenteDomain.Interfaces;
using Dapper;
using System.Data;

namespace ConCorrente.Infra.Data.Repositories;
public class ContaCorrenteRepository : IContaCorrenteRepository {
    private readonly IDbConnectionFactory _connectionFactory;
    public ContaCorrenteRepository(IDbConnectionFactory connectionFactory) {
        this._connectionFactory = connectionFactory;
    }

    public async Task<ContaCorrente> GetById(string id) {
        const string sql = "SELECT * FROM contacorrente WHERE idcontacorrente = @id";

        using (var connection = _connectionFactory.CreateConnection()) {
            return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { id });
        }
    }

    public async Task<IEnumerable<ContaCorrente>> GetContaCorrentes() {
        const string sql = "SELECT * FROM contacorrente";

        using (var connection = _connectionFactory.CreateConnection()) {
            return await connection.QueryAsync<ContaCorrente>(sql);
        }
    }
}

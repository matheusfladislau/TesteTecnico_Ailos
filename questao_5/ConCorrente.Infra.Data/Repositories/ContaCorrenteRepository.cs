using ConCorrente.Infra.Data.Interfaces;
using ConCorrenteDomain.Entities;
using ConCorrenteDomain.Interfaces;
using Dapper;

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

    public async Task<decimal> GetSaldoContaCorrenteAsync(string id) {
        decimal? qtCredito = 0;
        decimal? qtDebito = 0;

        const string sqlCredito = "SELECT SUM(valor) FROM movimento " +
                                  "WHERE idcontacorrente = @id " +
                                  "AND tipomovimento = 'C' ";

        const string sqlDebito = "SELECT SUM(valor) FROM movimento " +
                                 "WHERE idcontacorrente = @id " +
                                 "AND tipomovimento = 'D' ";

        using (var connection = _connectionFactory.CreateConnection()) {
            qtCredito = await connection.QueryFirstOrDefaultAsync<decimal?>(sqlCredito, new { id }) ?? 0;
            qtDebito = await connection.QueryFirstOrDefaultAsync<decimal?>(sqlDebito, new { id }) ?? 0;
        }

        var resultado = qtCredito - qtDebito;
        return (decimal)resultado;
    }
}

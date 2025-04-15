using ConCorrente.Infra.Data.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;

namespace ConCorrente.Infra.Data; 
public class SqliteConnectionFactory : IDbConnectionFactory {
    private readonly string _connectionString;
    public SqliteConnectionFactory(string connectionString) {
        this._connectionString = connectionString;
    }

    public IDbConnection CreateConnection() {
        return new SqliteConnection(_connectionString);
    }
}

using System.Data;

namespace ConCorrente.Infra.Data.Interfaces;
public interface IDbConnectionFactory {
    IDbConnection CreateConnection();
}

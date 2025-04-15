using ConCorrente.Application.Movimentos.Commands;
using ConCorrente.Domain.Interfaces;
using ConCorrente.Infra.Data;
using ConCorrente.Infra.Data.Interfaces;
using ConCorrente.Infra.Data.Repositories;
using ConCorrenteDomain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConCorrente.Infra.IoC; 
public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration) {
        services.AddSingleton<IDbConnectionFactory>(
            new SqliteConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
        services.AddScoped<IMovimentoRepository, MovimentoRepository>();
        services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(MovimentoCreateCommand).Assembly));
        return services;
    }
}

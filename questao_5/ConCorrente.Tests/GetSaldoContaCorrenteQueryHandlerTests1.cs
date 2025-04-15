using ConCorrente.Application.Saldos.Handlers;
using ConCorrente.Application.Saldos.Queries;
using ConCorrenteDomain.Entities;
using ConCorrenteDomain.Interfaces;
using FluentAssertions;
using NSubstitute;

namespace ConCorrente.Tests {
    public class GetSaldoContaCorrenteQueryHandlerTests1 {
        private readonly IContaCorrenteRepository _contaRepo = Substitute.For<IContaCorrenteRepository>();

        [Fact]
        public async Task Handle_ShouldReturnSaldo_ValidAccount() {
            var handler = new GetSaldoContaCorrenteQueryHandler(_contaRepo);
            var query = new GetSaldoContaCorrenteQuery("123");

            _contaRepo.GetById(query.IdContaCorrente).Returns(new ContaCorrente("123", 456, "Cliente 1", true));
            _contaRepo.GetSaldoContaCorrenteAsync(query.IdContaCorrente).Returns(500);

            var result = await handler.Handle(query, default);

            result.Should().Contain("\"saldo\":500");
        }
    }
}

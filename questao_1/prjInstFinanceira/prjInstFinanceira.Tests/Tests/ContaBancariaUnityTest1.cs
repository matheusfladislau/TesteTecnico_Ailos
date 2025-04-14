using prjInstFinanceira.Domain.Validation;

namespace prjInstFinanceira.Tests {
    public class ContaBancariaUnityTest1 {
        [Fact]
        public void SetNumeroConta_DeveAtribuir_QuandoNumeroValido() {
            var conta = new ContaBancaria();
            conta.SetNumeroConta(1234);
            Assert.Equal(1234, conta.NumeroConta);
        }

        [Fact]
        public void SetNumeroConta_DeveLancarExcecao_QuandoNumeroInvalido() {
            var conta = new ContaBancaria();
            var ex = Assert.Throws<ContaBancariaExceptionValidation>(() => conta.SetNumeroConta(0));
            Assert.Equal("Número da conta inválido.", ex.Message);
        }

        [Fact]
        public void SetTitularConta_DeveAtribuir_QuandoNomeValido() {
            var conta = new ContaBancaria();
            conta.SetTitularConta("João Silva");
            Assert.Equal("João Silva", conta.TitularConta);
        }

        [Fact]
        public void SetTitularConta_DeveLancarExcecao_QuandoNomeVazio() {
            var conta = new ContaBancaria();
            var ex = Assert.Throws<ContaBancariaExceptionValidation>(() => conta.SetTitularConta(""));
            Assert.Equal("Nome do titular da conta inválido.", ex.Message);
        }

        [Fact]
        public void SetSaldoInicial_DeveAtribuir_QuandoSaldoValido() {
            var conta = new ContaBancaria();
            conta.SetSaldoInicial(100m);
            Assert.Equal(100m, conta.Saldo);
        }

        [Fact]
        public void SetSaldoInicial_DeveLancarExcecao_QuandoSaldoNegativo() {
            var conta = new ContaBancaria();
            var ex = Assert.Throws<ContaBancariaExceptionValidation>(() => conta.SetSaldoInicial(-1m));
            Assert.Equal("Saldo inicial não pode ser negativo.", ex.Message);
        }

        [Fact]
        public void DepositarSaldo_DeveAumentarSaldo_QuandoValorValido() {
            var conta = new ContaBancaria();
            conta.SetSaldoInicial(100m);
            conta.DepositarSaldo(50m);
            Assert.Equal(150m, conta.Saldo);
        }

        [Fact]
        public void DepositarSaldo_DeveLancarExcecao_QuandoValorNegativo() {
            var conta = new ContaBancaria();
            var ex = Assert.Throws<ContaBancariaExceptionValidation>(() => conta.DepositarSaldo(0));
            Assert.Equal("Valor de depósito deve ser maior que zero.", ex.Message);
        }

        [Fact]
        public void SacarSaldo_DeveReduzirSaldoMaisTaxa_QuandoSaldoSuficiente() {
            var conta = new ContaBancaria();
            conta.SetSaldoInicial(100m);
            conta.SacarSaldo(20m);
            Assert.Equal(76.50m, conta.Saldo);
        }

        [Fact]
        public void SacarSaldo_DeveLancarExcecao_QuandoSaldoInsuficiente() {
            var conta = new ContaBancaria();
            conta.SetSaldoInicial(10m);
            var ex = Assert.Throws<ContaBancariaExceptionValidation>(() => conta.SacarSaldo(20m));
            Assert.Equal("Saldo insuficiente para saque.", ex.Message);
        }

        [Fact]
        public void SacarSaldo_DeveLancarExcecao_QuandoValorZero() {
            var conta = new ContaBancaria();
            conta.SetSaldoInicial(100m);
            var ex = Assert.Throws<ContaBancariaExceptionValidation>(() => conta.SacarSaldo(0m));
            Assert.Equal("Valor de saque deve ser maior que zero.", ex.Message);
        }
    }
}

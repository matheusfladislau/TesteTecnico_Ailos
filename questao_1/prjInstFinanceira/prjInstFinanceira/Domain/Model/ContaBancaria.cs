using prjInstFinanceira.Domain.Validation;

public sealed class ContaBancaria {
    public int NumeroConta { get; private set; }
    public string TitularConta { get; private set; }
    public decimal Saldo { get; private set; }

    private const decimal _TaxaSaque = 3.50m;

    public void SetNumeroConta(int numeroConta) {
        ValidarNumeroConta(numeroConta);
        this.NumeroConta = numeroConta;
    }

    public void SetTitularConta(string titularConta) {
        ValidarTitularConta(titularConta);
        this.TitularConta = titularConta;
    }

    public void SetSaldoInicial(decimal saldoConta) {
        ValidarSaldoInicial(saldoConta);
        this.Saldo = saldoConta;
    }

    public void DepositarSaldo(decimal valorDeposito) {
        ContaBancariaExceptionValidation.When(valorDeposito <= 0, 
            "Valor de depósito deve ser maior que zero.");

        this.Saldo += valorDeposito;
    }

    public void SacarSaldo(decimal valorSaque) {
        ContaBancariaExceptionValidation.When(valorSaque <= 0, 
            "Valor de saque deve ser maior que zero.");

        ContaBancariaExceptionValidation.When(valorSaque > this.Saldo, 
            "Saldo insuficiente para saque.");

        this.Saldo -= valorSaque + _TaxaSaque;
    }

    private void ValidarNumeroConta(int numeroConta) {
        ContaBancariaExceptionValidation.When(numeroConta <= 0,
                "Número da conta inválido.");
    }
    private void ValidarTitularConta(string titularConta) {
        ContaBancariaExceptionValidation.When(string.IsNullOrEmpty(titularConta),
                "Nome do titular da conta inválido.");
    }
    private void ValidarSaldoInicial(decimal saldoInicial) {
        ContaBancariaExceptionValidation.When(saldoInicial < 0,
                "Saldo inicial não pode ser negativo.");
    }
}
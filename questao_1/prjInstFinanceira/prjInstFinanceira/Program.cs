using prjInstFinanceira.Domain.Validation;
using System.Globalization;

static class Program {
    static void Main(string[] args) {
        var contaBancaria = new ContaBancaria();
        SetarDados(contaBancaria);

        int opcao = 0;
        while (true) {
            Console.Clear();
            Console.WriteLine("Menu");
            Console.WriteLine("1. Depositar");
            Console.WriteLine("2. Sacar");
            Console.WriteLine("3. Ver dados");
            Console.WriteLine("4. Alterar titular da conta");
            Console.WriteLine("5. Sair");
            Console.Write("Escreva o número: ");

            while (!int.TryParse(Console.ReadLine(), out opcao)) {
                Console.Write("Escreva um número válido! Reescreva: ");
            }

            switch (opcao) {
                case 1:
                    Console.Clear();
                    Depositar(contaBancaria);
                    break;
                case 2:
                    Console.Clear();
                    Sacar(contaBancaria);
                    break;
                case 3:
                    Console.Clear();
                    ExibirDados(contaBancaria);
                    break;
                case 4:
                    Console.Clear();
                    AlterarNomeTitular(contaBancaria);
                    break;
                case 5:
                    Console.WriteLine("Aperte qualquer tecla para sair.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Número entre 1-5! Reescreva: ");
                    break;
            }
        }
    }

    private static void AlterarNomeTitular(ContaBancaria contaBancaria) {
        bool dadosValidos = false;

        do {
            Console.Write("Digite o novo nome para o titular da conta: ");
            string nomeTitularNovo = Console.ReadLine()!;

            if (!Confirmar($"{contaBancaria.TitularConta} -> {nomeTitularNovo}. Tem certeza?")) {
                Console.WriteLine("Abortado.");
                Console.ReadKey();
                return;
            }
            try {
                contaBancaria.SetTitularConta(nomeTitularNovo);
                dadosValidos = true;
            } catch (ContaBancariaExceptionValidation ex) {
                Console.WriteLine($"{ex.GetBaseException().Message}");
            }
        } while (!dadosValidos);
    }

    private static void Sacar(ContaBancaria contaBancaria) {
        decimal saque = 0;
        bool dadosValidos = false;

        do {
            try {
                Console.Write("Entre um valor para saque (ex: 3.50): ");

                while (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out saque)) {
                    Console.WriteLine("Escreva um valor númerico correto! Reescreva: ");
                }

                contaBancaria.SacarSaldo(saque);
                ExibirDados(contaBancaria, true);
                dadosValidos = true;
            } catch (ContaBancariaExceptionValidation ex) {
                Console.WriteLine($"{ex.GetBaseException().Message}");
            }
        } while (!dadosValidos);
    }

    private static void Depositar(ContaBancaria contaBancaria) {
        decimal deposito = 0;
        bool dadosValidos = false;

        do {
            try {
                Console.Write("Entre um valor para depósito: ");

                while (!decimal.TryParse(Console.ReadLine()!, out deposito)) {
                    Console.WriteLine("Escreva um valor númerico correto! Reescreva: ");
                }

                contaBancaria.DepositarSaldo(deposito);
                ExibirDados(contaBancaria, true);
                dadosValidos = true;
            } catch (ContaBancariaExceptionValidation ex) {
                Console.WriteLine($"{ex.GetBaseException().Message}");
            }
        } while (!dadosValidos);
    }

    private static void SetarDados(ContaBancaria contaBancaria) {
        SetarNumeroConta(contaBancaria);
        SetarTitularConta(contaBancaria);
        SetarDepositoInicial(contaBancaria);
    }

    private static void SetarNumeroConta(ContaBancaria contaBancaria) {
        int numeroConta = 0;
        bool dadosValidos = false;

        do {
            try {
                Console.Write("Entre com o número da conta: ");
                while (!int.TryParse(Console.ReadLine(), out numeroConta)) {
                    Console.Write("Escreva um número válido! Reescreva: ");
                }

                contaBancaria.SetNumeroConta(numeroConta);
                dadosValidos = true;
            } catch (ContaBancariaExceptionValidation ex) {
                Console.WriteLine($"{ex.GetBaseException().Message}");
            }
        } while (!dadosValidos);
    }

    private static void SetarTitularConta(ContaBancaria contaBancaria) {
        string titularConta = string.Empty;
        bool dadosValidos = false;

        do {
            try {
                Console.Write("Entre com o titular da conta: ");
                titularConta = Console.ReadLine()!;
                contaBancaria.SetTitularConta(titularConta);
                dadosValidos = true;
            } catch (ContaBancariaExceptionValidation ex) {
                Console.WriteLine($"{ex.GetBaseException().Message}");
            }
        } while (!dadosValidos);
    }

    private static void SetarDepositoInicial(ContaBancaria contaBancaria) {
        decimal depositoInicial = 0;
        if (!Confirmar("Haverá depósito inicial?")) {
            contaBancaria.SetSaldoInicial(depositoInicial);
            return;
        }

        bool dadosValidos = false;
        do {
            try {
                Console.WriteLine("Entre o valor de depósito inicial: ");

                while (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out depositoInicial)) {
                    Console.WriteLine("Escreva um valor númerico correto! Reescreva: ");
                }

                contaBancaria.SetSaldoInicial(depositoInicial);
                dadosValidos = true;
            } catch (ContaBancariaExceptionValidation ex) {
                Console.WriteLine($"{ex.GetBaseException().Message}");
            }
        } while (!dadosValidos);
    }

    private static void ExibirDados(ContaBancaria contaBancaria, bool icAtt = false) {
        if (icAtt) {
            Console.WriteLine($"Dados da conta atualizados: ");
        } else {
            Console.WriteLine($"Dados da conta: ");
        }

        Console.WriteLine($"Conta: {contaBancaria.NumeroConta}, " +
            $"Titular: {contaBancaria.TitularConta}, " +
            $"Saldo: $ {contaBancaria.Saldo.ToString("F2", CultureInfo.InvariantCulture)}");
        Console.ReadKey();
    }

    private static bool Confirmar(string mensagem) {
        Console.Write($"{mensagem} (S/N): ");
        string resposta = Console.ReadLine()!;
        while (resposta != "S" && resposta != "s" && resposta != "N" && resposta != "n") {
            Console.Write("Apenas (S/N)! Reescreva: ");
            resposta = Console.ReadLine()!;
        }
        return resposta.ToUpper() == "S";
    }
}
using prjMovimentacaoConta.Models;
static class Program {
    async static Task Main(string[] args) {
        string outraRequisicao = string.Empty;

        do {
            Console.Clear();
            Console.Write("Digite o ID da Conta Corrente: ");
            var idContaCorrente = Console.ReadLine()!;

            Console.Write("Digite o tipo de movimento (C para Crédito, D para Débito): ");
            string tipoMovimento = Console.ReadLine()!.ToUpper();
            while (tipoMovimento != "C" && tipoMovimento != "D") {
                Console.WriteLine("Apenas (C/D)! Reescreva: ");
                tipoMovimento = Console.ReadLine()!.ToUpper();
            }

            Console.Write("Digite o valor do Movimento: ");
            var valor = decimal.Parse(Console.ReadLine()!);

            var idRequisicao = Guid.NewGuid().ToString();
            Console.WriteLine("ID da Requisição Gerado: " + idRequisicao);

            var movimentoRequest = new MovimentoRequest {
                IdRequisicao = idRequisicao,
                IdContaCorrente = idContaCorrente,
                TipoMovimento = tipoMovimento,
                Valor = valor
            };

            var client = new MovimentoClient();
            await client.SendMovimentoRequest(movimentoRequest);

            Console.Write("Fazer outra requisição? (S/N): ");
            outraRequisicao = Console.ReadLine()!.ToUpper();
            while (outraRequisicao != "S" && outraRequisicao != "N") {
                Console.WriteLine("Apenas (S/N)! Reescreva: ");
                outraRequisicao = Console.ReadLine()!.ToUpper();
            }
        } while (outraRequisicao == "S");
    } 
} 
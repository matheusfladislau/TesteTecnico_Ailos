using prjSaldoConta.Models;

static class Program {
    async static Task Main(string[] args) {
        string outraRequisicao = string.Empty;

        do {
            Console.Clear();
            Console.Write("Digite o ID da Conta Corrente: ");
            var idContaCorrente = Console.ReadLine()!;

            var saldoRequest = new SaldoRequest { IdContaCorrente = idContaCorrente };
            var client = new SaldoClient();
            await client.SendSaldoRequest(saldoRequest);

            Console.Write("Fazer outra requisição? (S/N): ");
            outraRequisicao = Console.ReadLine()!.ToUpper();
            while (outraRequisicao != "S" && outraRequisicao != "N") {
                Console.WriteLine("Apenas (S/N)! Reescreva: ");
                outraRequisicao = Console.ReadLine()!.ToUpper();
            }
        } while (outraRequisicao == "S");
    }
}
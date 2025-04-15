using prjMovimentacaoConta.Services;
using System;
namespace prjMovimentacaoConta.Models; 
public sealed class MovimentoClient {
    private readonly MovimentoService _movimentoService;
    private const int RetryDelayMilliseconds = 2000;

    public MovimentoClient() {
        _movimentoService = new MovimentoService();
    }

    public async Task SendMovimentoRequest(MovimentoRequest movimento) {
        int attempt = 0;

        while (true) {
            try {
                var response = await _movimentoService.SendMovimentoAsync(movimento);
                if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                    Console.WriteLine($"Resposta da API: {response.MensagemRetorno}. StatusCode: {response.StatusCode}");
                    break;
                }
                Console.WriteLine($"Erro na requisição. StatusCode: {response.StatusCode}, Mensagem: {response.MensagemRetorno}");
                break;
            } catch (Exception ex) {
                attempt++;
                Console.WriteLine($"Falha ao tentar enviar a requisição. Tentativa {attempt}. Erro: {ex.GetBaseException().Message}");

                Console.WriteLine($"Tentando novamente em {RetryDelayMilliseconds / 1000} segundos...");
                await Task.Delay(RetryDelayMilliseconds);
            }
        }
    }
}

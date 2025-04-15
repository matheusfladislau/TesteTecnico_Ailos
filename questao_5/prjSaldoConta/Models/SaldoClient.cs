using prjSaldoConta.Services;

namespace prjSaldoConta.Models;
public sealed class SaldoClient {
    private readonly SaldoService _saldoService;

    public SaldoClient() {
        _saldoService = new SaldoService();
    }

    public async Task SendSaldoRequest(SaldoRequest saldo) {
        try {
        var response = await _saldoService.GetSaldoContaCorrenteAsync(saldo);
            Console.WriteLine($"Resposta da API: {response.MensagemRetorno}. StatusCode: {response.StatusCode}");
        } catch (Exception ex) {
            Console.WriteLine($"Falha ao tentar enviar a requisição. Erro: {ex.GetBaseException().Message}");
        }
    }
}

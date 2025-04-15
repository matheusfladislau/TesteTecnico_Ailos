using prjSaldoConta.Models;
using System.Text.Json;

namespace prjSaldoConta.Services; 
public sealed class SaldoService {
    private readonly HttpClient _httpClient;
    public SaldoService() {
        _httpClient = new HttpClient();
    }

    public async Task<SaldoResponse> GetSaldoContaCorrenteAsync(SaldoRequest saldoRequest) {
        var url = $"https://localhost:7259/api/Saldos/GetSaldoContaCorrente/{saldoRequest.IdContaCorrente}";
        var response = await _httpClient.GetAsync(url);

        var contentResponse = await response.Content.ReadAsStringAsync();
        return new SaldoResponse(contentResponse, response.StatusCode != System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}

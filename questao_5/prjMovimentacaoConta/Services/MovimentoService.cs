using prjMovimentacaoConta.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace prjMovimentacaoConta.Services; 
public sealed class MovimentoService {
    private readonly HttpClient _httpClient;
    public MovimentoService() {
        _httpClient = new HttpClient();
    }

    public async Task<MovimentoResponse> SendMovimentoAsync(MovimentoRequest movimentoRequest) {
        var url = "https://localhost:7259/api/Movimentos";
        var jsonContent = JsonSerializer.Serialize(movimentoRequest);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        var contentResponse = await response.Content.ReadAsStringAsync();
        return new MovimentoResponse(contentResponse, response.StatusCode != System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}

using prjGolsAno.Models;
using System.Net.Http.Json;

namespace prjGolsAno.Services;
public sealed class FutebolService {
    private readonly HttpClient _httpClient;
    public FutebolService() {
        _httpClient = new HttpClient();
    }

    public async Task<int> GolsTotaisTimeAnoAsync(string nomeTime, int ano) {
        var responseGolsCasa = await GetGolsAsync(nomeTime, ano);
        if (responseGolsCasa.IsErro) {
            throw new Exception(responseGolsCasa.MensagemRetorno);
        }

        var responseGolsForaDeCasa = await GetGolsAsync(nomeTime, ano, false);
        if (responseGolsForaDeCasa.IsErro) {
            throw new Exception(responseGolsForaDeCasa.MensagemRetorno);
        }

        return responseGolsCasa.QuantidadeGols + responseGolsForaDeCasa.QuantidadeGols;
    }

    private async Task<ResponseClient> GetGolsAsync(string nomeTime, int ano, bool emCasa = true) {
        int paginaAtual = 1;
        int totalPaginas = 1;
        string paramTime = (emCasa)
            ? "team1"
            : "team2";
        int gols = 0;

        while (paginaAtual <= totalPaginas) {
            var request = BuildRequest(HttpMethod.Get,
                $"https://jsonmock.hackerrank.com/api/football_matches?year={ano}&{paramTime}={nomeTime}&page={paginaAtual}");
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) {
                return new ResponseClient($"Erro ao buscar dados da API. Detalhes: {response.ReasonPhrase}", true);
            }

            Task<ResponseAPI?> contentResponse = null;
            try {
                contentResponse = response.Content.ReadFromJsonAsync<ResponseAPI>();
            } catch (Exception ex) {
                return new ResponseClient($"Erro ao deserializar dados da API. Detalhes: {ex.GetBaseException().Message}", true);
            }

            if (contentResponse != null) {
                totalPaginas = contentResponse.Result.total_pages;
                foreach (var item in contentResponse.Result.data) {
                    gols += (emCasa)
                        ? item.team1goals
                        : item.team2goals;
                }
            }
            ++paginaAtual;
        }
        return new ResponseClient($"Sucesso.", false, gols);
    }

    private HttpRequestMessage BuildRequest(HttpMethod method, string uri) {
        return new HttpRequestMessage {
            Method = method,
            RequestUri = new Uri(uri)
        };
    }
}

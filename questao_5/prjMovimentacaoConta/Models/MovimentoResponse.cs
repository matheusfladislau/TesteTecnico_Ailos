using System.Net;

namespace prjMovimentacaoConta.Models; 
public sealed class MovimentoResponse {
    public MovimentoResponse(string mensagemRetorno, bool isErro, HttpStatusCode statusCode) {
        this.MensagemRetorno = mensagemRetorno;
        this.IsErro = isErro;
        this.StatusCode = statusCode;
    }

    public string MensagemRetorno { get; private set; }
    public bool IsErro { get; private set; }
    public HttpStatusCode StatusCode { get; private set; }
}

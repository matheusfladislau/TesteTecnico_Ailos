namespace prjGolsAno.Models;
public sealed class ResponseClient {
    public ResponseClient(string mensagemRetorno, bool isErro, int quantidadeGols = 0) {
        this.MensagemRetorno = mensagemRetorno;
        this.IsErro = isErro;
        this.QuantidadeGols = quantidadeGols;   
    }

    public string MensagemRetorno { get; private set; }
    public bool IsErro { get; private set; }
    public int QuantidadeGols { get; private set; }
}

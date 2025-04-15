namespace prjMovimentacaoConta.Models;
public sealed class MovimentoRequest {
    public string TipoMovimento { get; set; }
    public decimal Valor { get; set; }
    public string IdContaCorrente { get; set; }
    public string IdRequisicao { get; set; }
}

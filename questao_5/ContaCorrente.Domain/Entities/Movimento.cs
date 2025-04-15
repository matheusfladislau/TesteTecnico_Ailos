using ConCorrenteDomain.Validation;

namespace ConCorrenteDomain.Entities; 
public sealed class Movimento {
    public Movimento(string id, DateTime dataMovimento, string tipoMovimento, decimal valor, string idContaCorrente) {
        ValidateTipoMovimento(tipoMovimento);
        ValidateValor(valor);

        this.IdMovimento = id;
        this.DataMovimento = dataMovimento;
        this.TipoMovimento = tipoMovimento;
        this.Valor = valor;
        this.IdContaCorrente = idContaCorrente;
    }

    public Movimento(DateTime dataMovimento, string tipoMovimento, decimal valor, string idContaCorrente) {
        ValidateTipoMovimento(tipoMovimento);
        ValidateValor(valor);

        this.DataMovimento = dataMovimento;
        this.TipoMovimento = tipoMovimento;
        this.Valor = valor;
        this.IdContaCorrente = idContaCorrente;
    }

    public string IdMovimento { get; private set; }
    public DateTime DataMovimento { get; private set; }
    public string TipoMovimento { get; private set; }
    public decimal Valor { get; private set; }

    public string IdContaCorrente { get; set; }
    public ContaCorrente ContaCorrente { get; set; }

    private void ValidateValor(decimal valor) {
        MovimentoEntityValidation.When(valor <= 0, 
            "Valor deve ser positivo.", 
            "INVALID_VALUE");
    }

    private void ValidateTipoMovimento(string tipoMovimento) {
        MovimentoEntityValidation.When(tipoMovimento != "C" && tipoMovimento != "D"
            , "Tipo inválido."
            , "INVALID_TYPE");
    }
}

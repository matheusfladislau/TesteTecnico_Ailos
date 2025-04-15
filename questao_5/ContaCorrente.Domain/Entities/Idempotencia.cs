using ConCorrenteDomain.Entities;
using ConCorrenteDomain.Validation;

namespace ConCorrente.Domain.Entities; 
public sealed class Idempotencia {
    public Idempotencia(string id, string requisicao, string resultado) {
        ValidateRequisicao(requisicao);
        ValidateResultado(resultado);

        this.Chave_Idempotencia = id;
        this.Requisicao = requisicao;
        this.Resultado = resultado;
    }
    public string Chave_Idempotencia { get; private set; }
    public string Requisicao { get; private set; }
    public string Resultado { get; private set; }

    private void ValidateResultado(string resultado) {
        IdempotenciaExceptionValidation.When(string.IsNullOrEmpty(resultado),
           "Resultado é obrigatório.");

        IdempotenciaExceptionValidation.When(resultado.Length > 1000,
            "Resultado deve ter menos que 100 caracteres.");
    }
    private void ValidateRequisicao(string requisicao) {
        IdempotenciaExceptionValidation.When(string.IsNullOrEmpty(requisicao),
           "Requisição é obrigatória.");

        IdempotenciaExceptionValidation.When(requisicao.Length > 1000,
            "Requisição deve ter menos que 100 caracteres.");
    }
}

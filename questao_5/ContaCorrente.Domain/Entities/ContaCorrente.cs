using ConCorrenteDomain.Validation;

namespace ConCorrenteDomain.Entities; 
public class ContaCorrente {
    public ContaCorrente() { }
    public string IdContaCorrente { get; private set; }
    public int Numero { get; private set; }
    public string Nome { get; private set; }
    public bool Ativo { get; private set; }

    public ContaCorrente(string id, int numero, string nome, bool ativo) {
        ValidateNumero(numero);
        ValidateNome(nome);

        this.IdContaCorrente = id;
        this.Numero = numero;
        this.Nome = nome;
        this.Ativo = ativo;
    }

    public ContaCorrente(int numero, string nome, bool ativo) {
        ValidateNumero(numero);
        ValidateNome(nome);

        this.Numero = numero;
        this.Nome = nome;
        this.Ativo = ativo;
    }

    public void UpdateAtivo(bool ativo) {
        this.Ativo = ativo;
    }

    private void ValidateNome(string nome) {
        ContaCorrenteEntityValidation.When(string.IsNullOrEmpty(nome),
            "Nome é obrigatório.");

        ContaCorrenteEntityValidation.When(nome.Length > 100,
            "Nome deve ter menos que 100 caracteres.");
    }

    private void ValidateNumero(int numero) {
        ContaCorrenteEntityValidation.When(numero < 0,
            "Número inválido. Deve ser positivo.");
    }
}

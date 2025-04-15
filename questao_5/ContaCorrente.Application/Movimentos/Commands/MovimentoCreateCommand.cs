using MediatR;

namespace ConCorrente.Application.Movimentos.Commands; 
public class MovimentoCreateCommand : IRequest<string> {
    public string TipoMovimento { get;set; }
    public decimal Valor { get; set; }
    public string IdContaCorrente { get; set; }
    public string IdRequisicao { get; set; }
}

using MediatR;

namespace ConCorrente.Application.Saldos.Queries;
public class GetSaldoContaCorrenteQuery : IRequest<string> {
    public GetSaldoContaCorrenteQuery(string id) {
        this.IdContaCorrente = id;
    }
    public string IdContaCorrente { get; private set; }
}

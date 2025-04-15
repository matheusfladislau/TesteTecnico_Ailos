namespace ConCorrenteDomain.Validation; 
public sealed class ContaCorrenteEntityValidation : Exception {
    public ContaCorrenteEntityValidation(string error) : base(error) { }
    public static void When(bool error, string message) {
        if (error) {
            throw new ContaCorrenteEntityValidation(message);
        }
    }
}

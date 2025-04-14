namespace prjInstFinanceira.Domain.Validation;
public class ContaBancariaExceptionValidation : Exception {
    public ContaBancariaExceptionValidation(string error) : base(error) { }

    public static void When(bool hasError, string error) {
        if (hasError) {
            throw new ContaBancariaExceptionValidation(error);
        }
    }
}

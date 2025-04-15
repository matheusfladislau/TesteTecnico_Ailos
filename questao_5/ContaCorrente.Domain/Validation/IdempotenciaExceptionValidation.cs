namespace ConCorrenteDomain.Validation; 
public sealed class IdempotenciaExceptionValidation : Exception {
    public IdempotenciaExceptionValidation(string error) : base(error) { }
    public static void When(bool error, string message) {
        if (error) {
            throw new IdempotenciaExceptionValidation(message);
        }
    }
}

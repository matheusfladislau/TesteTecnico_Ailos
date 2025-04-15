namespace ConCorrenteDomain.Validation;
public sealed class MovimentoEntityValidation : Exception {
    public string _ErrorType { get; private set; }
    public MovimentoEntityValidation(string error, string errorType) : base(error) { 
        this._ErrorType = errorType;
    }

    public static void When(bool condition, string message, string errorType) {
        if (condition) {
            throw new MovimentoEntityValidation(message, errorType);
        }
    }
}

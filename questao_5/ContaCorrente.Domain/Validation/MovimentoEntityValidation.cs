using ConCorrente.Domain.Exceptions;

namespace ConCorrenteDomain.Validation;
public sealed class MovimentoEntityValidation : Exception {
    public ErrorType ErrorType { get; private set; }
    public MovimentoEntityValidation(string error, ErrorType errorType) : base(error) { 
        this.ErrorType = errorType;
    }

    public static void When(bool condition, string message, ErrorType errorType) {
        if (condition) {
            throw new MovimentoEntityValidation(message, errorType);
        }
    }
}

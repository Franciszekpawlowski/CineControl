using CineControl.Common.Results;

namespace CineControl.OperatorPanel.Errors;

public static class AuthServiceErrors
{
    public static Error Failure => Error.Failure("client error");
    public static Error NotFound => Error.NotFound("user not found");
    public static Error AccessUnauthorized => Error.AccessUnauthorized("access unauthorized");
}

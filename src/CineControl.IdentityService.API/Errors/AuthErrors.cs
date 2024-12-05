using CineControl.Common.Results;

namespace CineControl.IdentityService.API.Errors;

public static class AuthErrors
{
    public static Error NotFound() =>
        Error.NotFound("User not found");

    public static Error Failure() =>
        Error.Failure("Connection Failure");

    public static Error AccessUnauthorized() =>
        Error.AccessUnauthorized("Access Unauthorized");

    public static Error Conflict() =>
        Error.Conflict("Conflict");

    public static Error UnprocessableEntity() =>
        Error.UnprocessableEntity("Unprocessable Entity");
}

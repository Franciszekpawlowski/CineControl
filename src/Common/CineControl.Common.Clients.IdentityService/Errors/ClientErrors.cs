using CineControl.Common.Results;

namespace CineControl.Common.Clients.AuthService.Errors;

public class ClientErrors
{
    public static Error Failure => Error.Failure("Client Error");
    public static Error NotFound => Error.NotFound("Not Found");
    public static Error AccessUnauthorized => Error.AccessUnauthorized("Access Unauthorized");
}
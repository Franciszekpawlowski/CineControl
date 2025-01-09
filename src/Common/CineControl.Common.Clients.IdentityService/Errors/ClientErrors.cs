using CineControl.Common.Results;

namespace CineControl.Common.Clients.IdentityService.Errors;

public class ClientErrors
{
    public static Error Failure => Error.Failure("IdentityServiceClient: Client Error");
    public static Error NotFound => Error.NotFound("IdentityServiceClient: Not Found");
    public static Error AccessUnauthorized => Error.AccessUnauthorized("IdentityServiceClient: Access Unauthorized");
}
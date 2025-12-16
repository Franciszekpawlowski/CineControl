using CineControl.Common.Results;

namespace CineControl.Common.Clients.Errors;

public class ClientErrors
{
    public static Error Failure => Error.Failure("Connection to service Error");
    public static Error NotFound => Error.NotFound("Service Not Found");
    public static Error AccessUnauthorized => Error.AccessUnauthorized("Service: Access Unauthorized");
    public static Error Deserialization => Error.Failure("Deserialization from client error");
    public static Error TimedOut => Error.Failure("TimedOut");
    public static Error Generic => Error.Failure("Generic Error");
    public static Error WithMessage(string message) => Error.Failure(message);
}
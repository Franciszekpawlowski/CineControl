using CineControl.Common.Results;

namespace CineControl.BookingService.Errors
{
    public static class BookingErrors
    {
        public static Error NotFound(string message) => Error.NotFound(message);
        public static Error Conflict(string message) => Error.Conflict(message);
        public static Error UnprocessableEntity(string message) => Error.UnprocessableEntity(message);
        public static Error AccessUnauthorized(string message) => Error.AccessUnauthorized(message);
        public static Error Failure(string message) => Error.Failure(message);
    }
}

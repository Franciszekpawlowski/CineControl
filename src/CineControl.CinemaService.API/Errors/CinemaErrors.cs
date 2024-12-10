using CineControl.Common.Results;

namespace CineControl.CinemaService.API.Errors
{
    public static class CinemaErrors
    {
        public static Error NotFound(string message) => Error.NotFound(message);
        public static Error CinemaNotFound(int id) => Error.NotFound($"Cinema with id {id} not found");
        public static Error TheaterNotFound(int id) => Error.NotFound($"Theater with id {id} not found");
        public static Error SeatNotFound(int id) => Error.NotFound($"Seat with id {id} not found");
        public static Error Conflict(string message) => Error.Conflict(message);
        public static Error UnprocessableEntity(string message) => Error.UnprocessableEntity(message);
        public static Error AccessUnauthorized(string message) => Error.AccessUnauthorized(message);
        public static Error BadRequest(string message) => Error.BadRequest(message);
        public static Error MissingTenantHeader() => Error.BadRequest("No tenant specified");
        public static Error Failure(string message) => Error.Failure(message);
    }
}

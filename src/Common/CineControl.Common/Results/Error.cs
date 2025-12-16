namespace CineControl.Common.Results
{
    public class Error
    {
        private Error(string description,ErrorType errorType)
        {
            Description = description;
            ErrorType = errorType;
        }

        public string Description { get; }
        public ErrorType ErrorType { get; }

        public static Error Failure(string description) => 
            new(description, ErrorType.Failure);

        public static Error NotFound(string description) => 
            new(description, ErrorType.NotFound);

        public static Error AccessUnauthorized(string description) => 
            new(description, ErrorType.AccessUnauthorized);

        public static Error Conflict(string description) => 
            new(description, ErrorType.Conflict);

        public static Error UnprocessableEntity(string description) => 
            new(description, ErrorType.UnprocessableEntity);

        public static Error BadRequest(string description) => 
            new(description, ErrorType.BadRequest);
    }
}
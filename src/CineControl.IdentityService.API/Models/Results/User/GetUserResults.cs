namespace CineControl.IdentityService.API.Models.Results.User
{
    public class GetUserResults
    {
        public ApplicationUser User { get; } 
        public GetUserResults() {}
        public GetUserResults(ApplicationUser user)
        {
            User = user;
        }
    }
}

namespace CineControl.IdentityService.API.Models.Results.User
{
    public class GetUserResults
    {
        public ApplicationUser User { get; }
        public Common.Enums.Roles Role { get; }
        public GetUserResults() { }
        public GetUserResults(ApplicationUser User, Common.Enums.Roles Role)
        {
            this.User = User;
            this.Role = Role;
        }
    }
}

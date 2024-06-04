using CineControl.Domain.Common;

namespace CineControl.Domain.Users;

public class Users : Entity
{   
    public string FirstName { get; } = string.Empty;
    public string LastName { get;  } = string.Empty;
    public string Email { get; } = string.Empty;
    public Users(
        Guid id,
        string firstName,
        string lastName,
        string email
    ){
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    private Users() { }
}

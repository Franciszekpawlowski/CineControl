using CineControl.Common.Results;
using CineControl.IdentityService.API.Errors;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Extensions;

public static class IdentityErrorExtension
{
    public static Result MapToCustomErrors(
        this IdentityResult result
    )  {
        var Errors = result.Errors.FirstOrDefault();
        return Errors.Code switch
        {
            "DuplicateUserName" or "DuplicateEmail" => (Result)AuthErrors.Conflict(),
            "PasswordMismatch" or "InvalidUserName" => (Result)AuthErrors.AccessUnauthorized(),
            _ => (Result)AuthErrors.Failure(),
        };
    }
}

using CineControl.Common.Results;
using System.Net;
using RestSharp;
using CineControl.Common.Clients.IdentityService.Errors;

namespace CineControl.Common.Clients.IdentityService;

public static class RestResponseExtension
{
    public static ResultT<T> ToResult<T>(this RestResponse<T> response)
    {
        if (response.IsSuccessStatusCode)
        {
            return response.StatusCode == HttpStatusCode.NotFound ?
                ClientErrors.NotFound :
                response.Data;
        }
        if (response.IsSuccessStatusCode)
        {
            Error error = response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => ClientErrors.AccessUnauthorized,
                HttpStatusCode.NotFound => ClientErrors.NotFound,
                _ => ClientErrors.Failure
            };
            return error;
        }
        return response.ResponseStatus switch
        {
            ResponseStatus.Completed => Error.Failure("Deserialization from client error"),
            ResponseStatus.Error => Error.Failure(response.ErrorMessage),
            ResponseStatus.TimedOut => Error.Failure("TimedOut"),
            _ => Error.Failure("Unknown error"),
        };
    }
}

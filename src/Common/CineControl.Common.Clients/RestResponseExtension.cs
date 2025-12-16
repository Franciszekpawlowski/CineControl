using System.Net;
using CineControl.Common.Clients.Errors;
using CineControl.Common.Results;
using RestSharp;

namespace CineControl.Common.Clients;

public static class RestResponseExtension
{
    public static ResultT<T> ToResult<T>(this RestResponse<T> response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode != HttpStatusCode.NotFound)
            {
                return response.Data;
            }
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
            ResponseStatus.Completed => ClientErrors.Deserialization,
            ResponseStatus.Error => ClientErrors.WithMessage(response.ErrorMessage),
            ResponseStatus.TimedOut => ClientErrors.TimedOut,
            _ => ClientErrors.Generic,
        };
    }

        public static Result ToResult(this RestResponse response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode != HttpStatusCode.NotFound)
            {
                return Result.Success();
            }
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
            ResponseStatus.Completed => ClientErrors.Deserialization,
            ResponseStatus.Error => ClientErrors.WithMessage(response.ErrorMessage),
            ResponseStatus.TimedOut => ClientErrors.TimedOut,
            _ => ClientErrors.Generic,
        };
    }

}

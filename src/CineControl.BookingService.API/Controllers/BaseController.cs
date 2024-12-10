using CineControl.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.BookingService.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult Problem(Error error)
        {
            var statusCode = error.ErrorType switch
            {
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.AccessUnauthorized => StatusCodes.Status403Forbidden,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.UnprocessableEntity => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                statusCode: statusCode,
                detail: error.Description
            );
        }
    }
}

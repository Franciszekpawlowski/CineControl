using CineControl.IdentityService.API.Models.Response;
using CineControl.IdentityService.API.Models.Results;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    public class BaseController : ControllerBase
    {

        public BaseController()
        {

        }

        protected IActionResult Error<T>(GenericResults<T> error)
        {
            var respone = new ErrorResponse() {
                Errors = error.Errors
            };
            return BadRequest(respone);
        }
    }
}
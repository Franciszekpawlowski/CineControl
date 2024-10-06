using CineControl.IdentityService.API.Models.Response.Base;

namespace CineControl.IdentityService.API.Models.Response
{
    public class ErrorResponse
    {
        public IEnumerable<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
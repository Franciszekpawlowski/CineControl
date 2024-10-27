using CineControl.IdentityService.API.Models.Response.Base;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Models.Results
{
    public class GenericResults<T>
    {
        public T Data { get; set; }
        public List<ErrorModel> Errors { get; set; }
        public bool IsSuccess => Errors is null || Errors.Count == 0;

        public GenericResults<T> AddError(string Message)
        {
            Errors = Errors ?? new List<ErrorModel>();
            Errors.Add(new ErrorModel() { Message = Message });
            return this;
        }

        public GenericResults<T> AddErrors(IEnumerable<string> Messages)
        {
            foreach (var message in Messages)
            {
                AddError(message);
            }
            return this;
        }

        public GenericResults<T> AddErrors(IdentityResult identityResult)
        {
            AddErrors(identityResult.Errors.Select(error => error.Description));
            return this;
        }

        public void SetData(T data)
        {
            Data = data;
        }
    }
}
using CineControl.IdentityService.API.Models.Response.Base;

namespace CineControl.IdentityService.API.Models.Results
{
    public class GenericResults<T>
    {
        public T Data { get; set; }
        public IEnumerable<ErrorModel> Errors { get; set; }
        public bool IsSuccess => Errors is null || Errors.Any();

        public void AddError(string Message)
        {
            Errors.Append(new ErrorModel() { Message = Message });
        }

        public void AddErrors(IEnumerable<string> Messages)
        {
            foreach (var message in Messages)
            {
                AddError(message);
            }
        }

        public void SetData(T data)
        {
            Data = data;
        }
    }
}
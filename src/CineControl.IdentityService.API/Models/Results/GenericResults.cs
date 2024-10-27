using CineControl.IdentityService.API.Models.Response.Base;

namespace CineControl.IdentityService.API.Models.Results
{
    public class GenericResults<T>
    {
        public T Data { get; set; }
        public List<ErrorModel> Errors { get; set; }
        public bool IsSuccess => Errors is null || Errors.Count == 0;

        public void AddError(string Message)
        {
            Errors = Errors ?? new List<ErrorModel>();
            Errors.Add(new ErrorModel() { Message = Message });
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
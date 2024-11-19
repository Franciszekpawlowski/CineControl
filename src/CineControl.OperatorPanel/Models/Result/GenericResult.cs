namespace CineControl.OperatorPanel.Models.Result
{
    public class GenericResult<T>
    {
        public T Data { get; set; }
    
        public List<ErrorModel> Errors { get; set; }

        public bool IsSuccess  => Errors is null || Errors.Count == 0;

        public GenericResult<T> AddError(string Message)
        {
            Errors ??= new List<ErrorModel>();
            Errors.Add(new ErrorModel() { Message = Message });
            return this;
        }        
    }
}
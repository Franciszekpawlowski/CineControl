namespace CineControl.Common.Results
{
    public class ResultT<TValue> : Result
    {
        private readonly TValue? _value;
        protected ResultT(TValue? value): base()
        {
            _value = value;
        }

        protected ResultT(Error error): base(error)
        {
            _value = default;
        }
        public TValue? Value => 
            IsSuccess ? _value : throw new InvalidOperationException("Result is not successful");
        //public Error? Error { get;}

        public static implicit operator ResultT<TValue>(Error error) => new(error);
        public static implicit operator ResultT<TValue>(TValue value) => new(value);


        public static ResultT<TValue> Success(TValue value) => new(value);

        public static ResultT<TValue> Failure(Error error) => new(error);
    }
}
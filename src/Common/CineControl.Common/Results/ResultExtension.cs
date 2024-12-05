namespace CineControl.Common.Results;

public static class ResultExtension
{
    public static T Match<T>(
        this Result result, 
        Func<T> onSuccess, 
        Func<Error, T> onFailure) => 
            result.IsSuccess ? onSuccess() : onFailure(result.Error!);

    public static T Match<T,TValue>(
        this ResultT<TValue> result, 
        Func<TValue,T> onSuccess,
        Func<Error,T> onFailure) =>
            result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error!);
}

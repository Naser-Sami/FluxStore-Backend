namespace FluxStore.Application.Common;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }

    public static Result Failure(string message)
        => new Result { IsSuccess = false, Message = message };

    public static Result<T> Failure<T>(string message)
        => Result<T>.Failure(message);

    public static Result<T> Success<T>(T data, string? message = null)
        => Result<T>.Success(data, message);
}

public class Result<T> : Result
{
    public T? Data { get; set; }

    public new static Result<T> Failure(string message)
        => new Result<T> { IsSuccess = false, Message = message };

    public static Result<T> Success(T data, string? message = null)
        => new Result<T> { IsSuccess = true, Data = data, Message = message };
}
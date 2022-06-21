namespace main.Helpers;

public class ResultBase<T>
{
    public bool IsSuccess { get; set; }
    public T? Response { get; set; }
    public string? Error { get; set; }

    public static ResultBase<T> Success(T response) => new()
    {
        IsSuccess = true,
        Response = response,
    };

    public static ResultBase<T> Failure(string error) => new()
    {
        IsSuccess = false,
        Error = error
    };
}
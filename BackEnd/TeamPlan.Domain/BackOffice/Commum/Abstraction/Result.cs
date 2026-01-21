namespace TeamPlan.Domain.BackOffice.Commum.Abstraction;

public class Result
{
    public bool IsSuccess { get;private set; }
    public Error Error { get;private set; }

    protected Result()
        => IsSuccess = true;

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result Success() => new();
    public static Result Failure(Error error) => new(error);


    public static implicit operator Result(Error error)
        => Failure(error);
}
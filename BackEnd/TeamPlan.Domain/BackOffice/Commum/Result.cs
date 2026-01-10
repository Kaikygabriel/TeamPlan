using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Domain.BackOffice.Commum;

public class Result<T>  : Result
{
    public T Value { get;private set; }

    private Result(T value) : base()
        => Value = value;

    private Result(Error error) : base(error)
    {
        
    }
    
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);

}
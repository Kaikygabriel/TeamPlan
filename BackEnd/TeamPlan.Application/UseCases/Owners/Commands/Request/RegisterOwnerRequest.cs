using MediatR;
using TeamPlan.Application.UseCases.Owners.Commands.Response;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Application.UseCases.Owners.Commands.Request;

public record RegisterOwnerRequest(string Name, string Email, string Password) : IRequest<Result<OwnerAuthResponse>>
{
    public Result<Owner> ToEntity()
    {
        var resultEmail = Domain.BackOffice.ValueObject.Email.Factories.Create(Email);
        if (!resultEmail.IsSuccess)
            return Result<Owner>.Failure(resultEmail.Error);
        var resultPassword = Domain.BackOffice.ValueObject.Password.Factory.Create(Password);
        if(!resultPassword.IsSuccess)
            return Result<Owner>.Failure(resultPassword.Error);
        
        var resultUser = User.Factories.Create(resultPassword.Value, resultEmail.Value);
        if (!resultEmail.IsSuccess)
            return Result<Owner>.Failure(resultUser.Error);
        
        var resultOwner = Owner.Factories.Create(Name,resultUser.Value);
        return resultOwner;
    }
}
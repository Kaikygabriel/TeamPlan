using MediatR;
using TeamPlan.Application.UseCases.Members.Command.Response;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Application.UseCases.Members.Command.Request;

public record RegisterMemberRequest(string Name, string Email, string Password) : IRequest<Result<AuthMemberResponse>>
{
    public Result<Member> ToEntity()
    {
        var resultEmail = Domain.BackOffice.ValueObject.Email.Factories.Create(Email);
        if (!resultEmail.IsSuccess)
            return Result<Member>.Failure(resultEmail.Error);
        
        var resultPassword = Domain.BackOffice.ValueObject.Password.Factory.Create(Password);
        if(!resultPassword.IsSuccess)
            return Result<Member>.Failure(resultPassword.Error);
        
        var resultUser = User.Factories.Create(resultPassword.Value, resultEmail.Value);
        if (!resultEmail.IsSuccess)
            return Result<Member>.Failure(resultUser.Error);
        
        var resulMember = Member.Factories.Create(Name,resultUser.Value);
        return resulMember;
    } 
}
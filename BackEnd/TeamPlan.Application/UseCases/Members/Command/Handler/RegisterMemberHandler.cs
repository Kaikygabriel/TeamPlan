using MediatR;
using TeamPlan.Application.UseCases.Members.Command.Request;
using TeamPlan.Application.UseCases.Members.Command.Response;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.UseCases.Members.Command.Handler;

internal class RegisterMemberHandler : HandlerBaseMemberAuth,IRequestHandler<RegisterMemberRequest,Result<AuthMemberResponse>>
{
    public RegisterMemberHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUserServiceAuth userServiceAuth) 
        : base(unitOfWork, tokenService)
    {
    }

    public async Task<Result<AuthMemberResponse>> Handle(RegisterMemberRequest request, CancellationToken cancellationToken)
    {
        var resultMemberCreate = request.ToEntity();
        if (!resultMemberCreate.IsSuccess)
            return Result<AuthMemberResponse>.Failure(resultMemberCreate.Error);
        
        var member = resultMemberCreate.Value;
        if (!await _unitOfWork.UserRepository.GetUserExistsByEmail(member.User.Email.Address))
            return Result<AuthMemberResponse>.Failure(new("user.exists", "user already exists!"));
        
        var token = GenerateAcessTokenByMember(member);
        var response = new AuthMemberResponse(token, member.Id);
        
        _unitOfWork.MemberRepository.Create(member);
        await _unitOfWork.CommitAsync();
        
        return Result<AuthMemberResponse>.Success(response);
    }
}
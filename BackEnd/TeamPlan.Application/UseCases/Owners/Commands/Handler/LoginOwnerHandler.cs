using MediatR;
using TeamPlan.Application.UseCases.Owners.Commands.Request;
using TeamPlan.Application.UseCases.Owners.Commands.Response;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.UseCases.Owners.Commands.Handler;

internal class LoginOwnerHandler : HandlerBaseOwnerAuth,
    IRequestHandler<LoginOwnerRequest,Result<OwnerAuthResponse>>
{
    public LoginOwnerHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUserServiceAuth userServiceAuth) : base(unitOfWork, tokenService, userServiceAuth)
    {
    }

    public async Task<Result<OwnerAuthResponse>> Handle(LoginOwnerRequest request, CancellationToken cancellationToken)
    {
        var resultUserAuth = await UserServiceAuth.VerifyUserIsValid(request.Email, request.Password);
        if (!resultUserAuth.IsSuccess)
            return Result<OwnerAuthResponse>.Failure(resultUserAuth.Error);
        var owner = await _unitOfWork.OwnerRepository.GetOwnerByEmail(resultUserAuth.Value.Email.Address);
        var token = GenerateAcessTokenByOwner(owner);
        return Result<OwnerAuthResponse>.Success(new OwnerAuthResponse(token, owner.Id));
    }
}
using MediatR;
using TeamPlan.Application.UseCases.Owners.Commands.Request;
using TeamPlan.Application.UseCases.Owners.Commands.Response;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.UseCases.Owners.Commands.Handler;

internal class RegisterOwnerHandler : HandlerBaseOwnerAuth,IRequestHandler<RegisterOwnerRequest,Result<OwnerAuthResponse>>
{
    public RegisterOwnerHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUserServiceAuth userServiceAuth) : base(unitOfWork, tokenService, userServiceAuth)
    {
    }

    public async Task<Result<OwnerAuthResponse>> Handle(RegisterOwnerRequest request, CancellationToken cancellationToken)
    {
        var resultCreateOwner = request.CreateOwner();
        if (!resultCreateOwner.IsSuccess)
            return Result<OwnerAuthResponse>.Failure(resultCreateOwner.Error);
        var resultCreateUser = await UserServiceAuth.CreateUser(resultCreateOwner.Value.User);
        if(!resultCreateUser.IsSuccess)
            return Result<OwnerAuthResponse>.Failure(resultCreateUser.Error);
        _unitOfWork.OwnerRepository.Create(resultCreateOwner.Value);
        await _unitOfWork.CommitAsync();

        var token = GenerateAcessTokenByOwner(resultCreateOwner.Value);
        var response = new OwnerAuthResponse(token , resultCreateOwner.Value.Id);
        return Result<OwnerAuthResponse>.Success(response);
    }
    
}

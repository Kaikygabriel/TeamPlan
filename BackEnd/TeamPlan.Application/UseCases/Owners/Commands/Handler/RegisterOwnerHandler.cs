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
        var resultCreateOwner = request.ToEntity();
        if (!resultCreateOwner.IsSuccess)
            return Result<OwnerAuthResponse>.Failure(resultCreateOwner.Error);
        
        var owner = resultCreateOwner.Value;
        if (!await _unitOfWork.UserRepository.GetUserExistsByEmail(owner.User.Email.Address))
            return Result<OwnerAuthResponse>.Failure(new("user.exists", "user already exists!"));
        
        _unitOfWork.OwnerRepository.Create(owner);
        await _unitOfWork.CommitAsync();

        var token = GenerateAcessTokenByOwner(owner);
        var response = new OwnerAuthResponse(token , owner.Id);
        return Result<OwnerAuthResponse>.Success(response);
    }
   
}

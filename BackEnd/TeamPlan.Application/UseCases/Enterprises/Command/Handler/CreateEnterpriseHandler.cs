using MediatR;
using TeamPlan.Application.UseCases.Enterprises.Command.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Enterprises.Command.Handler;

internal class CreateEnterpriseHandler: HandlerBase,IRequestHandler<CreateEnterpriseRequest,Result<Guid>>
{
    public CreateEnterpriseHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<Guid>> Handle(CreateEnterpriseRequest request, CancellationToken cancellationToken)
    {
        Owner? owner = await _unitOfWork.OwnerRepository.GetByPredicate(x=>x.Id == request.OwnerId);
        if (owner is null)
            return new Error("Owner.NoExisting", "Owner No Existing!");
        var resultCreateEnterprise = Enterprise.Factories.Create(request.Name, owner);
        if(!resultCreateEnterprise.IsSuccess)
            return new Error("Create.Enterprise", "Create enterprise invalid!");
        
        var enterprise = resultCreateEnterprise.Value;
        owner.EnterpriseId = enterprise.Id;
            
        _unitOfWork.EnterpriseRepository.Create(enterprise);
        _unitOfWork.OwnerRepository.Update(owner);
        
        await _unitOfWork.CommitAsync();
        return Result<Guid>.Success(enterprise.Id);
    }
} 
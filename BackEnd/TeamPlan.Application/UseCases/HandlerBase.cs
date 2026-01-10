using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases;

public abstract  class HandlerBase
{
    protected readonly IUnitOfWork _unitOfWork;

    protected HandlerBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
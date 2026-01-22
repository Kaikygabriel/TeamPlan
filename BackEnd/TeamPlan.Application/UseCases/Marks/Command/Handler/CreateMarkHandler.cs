using MediatR;
using TeamPlan.Application.UseCases.Marks.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Marks.Command.Handler;

internal class CreateMarkHandler : HandlerBase,IRequestHandler<CreateMarkRequest,Result>
{
    public CreateMarkHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(CreateMarkRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        if (team is null)
            return new Error("Team.NotFound", "Not found");
        
        var markCreateResult = request.ToEntity();
        if (!markCreateResult.IsSuccess)
            return markCreateResult.Error;
        
        var mark = markCreateResult.Value;
        
        team.AddMark(mark);
        _unitOfWork.TeamRepository.Update(team);
        _unitOfWork.MarkRepository.Create(mark);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}
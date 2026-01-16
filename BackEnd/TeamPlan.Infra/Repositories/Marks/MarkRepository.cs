using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Mark;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.Marks;

public class MarkRepository: Repository<Mark>,IMarkRepository
{
    public MarkRepository(AppDbContext context) : base(context)
    {
    }
}
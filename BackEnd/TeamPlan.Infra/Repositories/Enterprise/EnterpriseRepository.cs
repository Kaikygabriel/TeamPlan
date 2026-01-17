using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Enterprise;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.Enterprise;

public class EnterpriseRepository : Repository<Domain.BackOffice.Entities.Enterprise>,IEnterpriseRepository
{
    public EnterpriseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Domain.BackOffice.Entities.Enterprise?> GetByIdWithTasks(Guid id)
    {
        return await _context.Enterprises
            .Include(x=>x.Teams)
                .ThenInclude(x=>x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Team;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.Team;

public class TeamRepository  : Repository<Domain.BackOffice.Entities.Team>,ITeamRepository
{
    public TeamRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Domain.BackOffice.Entities.Team?> GetByIdWithMembersWithTask(Guid id)
    {
        return await _context
            .Teams
            .Include(x=>x.Manager)
            .Include(x=>x.Members)
            .Include(x=>x.Tasks)
            .Include(x=>x.Marks)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Domain.BackOffice.Entities.Team?> GetByIdWithMember(Guid id)
    {
        return  await _context
            .Teams
            .Include(x=>x.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
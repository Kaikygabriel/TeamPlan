using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Member;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.Member;

public class MemberRepository : Repository<Domain.BackOffice.Entities.Member>,IMemberRepository
{
    public MemberRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Domain.BackOffice.Entities.Member?> GetByEmail(string email)
    {
        return await _context.Members
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.User.Email.Address == email);
    }

    public async Task<Domain.BackOffice.Entities.Member?> GetByUserIdWithUser(Guid userId)
    {
        return await _context.Members
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.User.Id ==userId);
    }
}
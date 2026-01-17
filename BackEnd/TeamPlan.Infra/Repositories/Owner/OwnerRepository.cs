using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Owner;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.Owner;

public class OwnerRepository : Repository<Domain.BackOffice.Entities.Owner>,IOwnerRepository
{
    public OwnerRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Domain.BackOffice.Entities.Owner?> GetOwnerByEmail(string email)
    {
        return await _context.Owners
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.User.Email.Address == email);
    }
}
using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.User;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.User;

public class UserRepository : Repository<Domain.BackOffice.Entities.User>,IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> GetUserExistsByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Address == email);
        return user is null;
    }
}
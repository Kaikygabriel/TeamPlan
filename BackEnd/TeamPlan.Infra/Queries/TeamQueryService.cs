using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TeamPlan.Application.DTOs.StoreFront.Team;
using TeamPlan.Application.Interfaces.Queries;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Queries;

internal class TeamQueryService : ITeamQueryService
{
    private readonly AppDbContext _context;

    public TeamQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TeamDashBoardResponse> GetTeamDashBoardById(Guid id)
    {
        return await _context
            .Teams
            .AsNoTrackingWithIdentityResolution()
            .Where(x=>x.Id == id)
            .Select(x =>
                new TeamDashBoardResponse(
                    x.Enterprise.IdOwner,
                    x.ManagerId,
                    x.Members.Select(x=>x.Id),
                    x.Members.Select(x => x.Name),
                    x.Manager.Name,
                    x.Tasks.Where(x => x.Active),
                    x.PercentageByMonthCurrent,
                    x.Marks.Where(x => !x.Done))
            ).FirstOrDefaultAsync();
    }
    public async Task<Team?> GetTeamWithTasksAndKanban(Guid id)
    {
        return await _context
            .Teams
            .AsNoTracking()
            .Include(x => x.Tasks)
            .Include(x => x.Kanbans)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
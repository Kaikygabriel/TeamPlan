using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Application.DTOs.StoreFront.Team;

public record TeamViewDashBoardTaskInTeam
    (
        string Name,
        Guid ManagerId,
        IEnumerable<Domain.BackOffice.Entities.RecurringTask>RecurringTasks,
        IEnumerable<Kanban> Kanbans,
        ushort PercentageByMonthCurrent
    )
{
    public static explicit operator TeamViewDashBoardTaskInTeam(Domain.BackOffice.Entities.Team team)
        => new
        (
            team.Name,
            team.ManagerId,
            team.RecurringTasks,
            team.Kanbans,
            team.PercentageByMonthCurrent
        );
}
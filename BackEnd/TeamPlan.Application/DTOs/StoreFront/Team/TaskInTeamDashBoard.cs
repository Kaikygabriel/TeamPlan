using Task = TeamPlan.Domain.BackOffice.Entities.Task;

namespace TeamPlan.Application.DTOs.StoreFront.Team;

public record TaskInTeamDashBoard(Domain.BackOffice.Entities.Team Team,IEnumerable<Task>Tasks);
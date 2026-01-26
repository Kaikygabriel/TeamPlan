using TeamPlan.Application.DTOs.StoreFront.Tasks;

namespace TeamPlan.Application.DTOs.StoreFront.Team;

public record TaskInTeamDashBoard(TeamViewDashBoardTaskInTeam Team,IEnumerable<TaskViewDashBoardTaskInTeam>Tasks);
namespace TeamPlan.Application.DTOs.Enterprises;

public record EnterpriseDashBoardDTO
    (string NameEnterprise,IEnumerable<Domain.BackOffice.Entities.Team>Teams,ushort PercentageAverage);
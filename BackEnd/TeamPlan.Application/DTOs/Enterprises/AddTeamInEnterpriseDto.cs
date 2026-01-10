namespace TeamPlan.Application.DTOs.Enterprises;

public record AddTeamInEnterpriseDto(string Name,Guid ManagerId,Guid EnterpriseId,Guid OwnerId);
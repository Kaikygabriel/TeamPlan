namespace TeamPlan.Application.DTOs.Tasks;

public record CreateTaskDto(DateTime EndDate, string Title, string Description,Guid TeamId);
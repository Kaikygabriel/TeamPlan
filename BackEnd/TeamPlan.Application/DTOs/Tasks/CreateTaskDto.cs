using TeamPlan.Domain.BackOffice.Enum;

namespace TeamPlan.Application.DTOs.Tasks;

public record CreateTaskDto(DateTime EndDate, string Title, string Description,Guid TeamId,EPriority Priority);
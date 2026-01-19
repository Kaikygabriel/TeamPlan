using Task = TeamPlan.Domain.BackOffice.Entities.Task;

namespace TeamPlan.Application.DTOs.Tasks;

public record ReportTaskDto(int TasksDoneInMonth,IEnumerable<Task>Tasks);
using TeamPlan.Domain.BackOffice.Enum;

namespace TeamPlan.Application.DTOs.RecurringTask;

public record CreateRecurringTaskDto(string Title, string Description,ushort DayMonth,int DaysActiveTask,EPriority Priority);
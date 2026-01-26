using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Enum;
using Task = TeamPlan.Domain.BackOffice.Entities.Task;

namespace TeamPlan.Application.DTOs.StoreFront.Tasks;

public record TaskViewDashBoardTaskInTeam(
    Guid Id,
    DateTime CreateAt,
    DateTime EndDate,
    ushort Percentage,
    string Title,
    string Description,
    EPriority Priority,
    IEnumerable<Comment> Comments,
    ushort? KanbanCurrent,
    string? MemberName = "")
{
    public static IEnumerable<TaskViewDashBoardTaskInTeam> FromIEnumerableTaskView(IEnumerable<Domain.BackOffice.Entities.Task> tasks)
        => tasks.Select(x => (TaskViewDashBoardTaskInTeam)x);

    public static explicit operator TaskViewDashBoardTaskInTeam(Task task)
        => new(
            task.Id,
            task.CreateAt,
            task.EndDate,
            task.Percentage,
            task.Title,
            task.Description,
            task.Priority,
            task.Comments,
            task.KanbanCurrent,
            task.Member?.Name
        );
};
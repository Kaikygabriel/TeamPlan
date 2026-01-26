using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Application.DTOs.StoreFront.Tasks;

public record TaskDashBoardDto
    (
        string Title,
        string Description,
        DateTime CreateAt,
        DateTime DoneDate, string? MemberName,
        ushort Percentage,
        string TeamName,
        string? KanbanCurrent,
        IEnumerable<string>? Kanbans,
        IEnumerable<Comment>Comments
    );
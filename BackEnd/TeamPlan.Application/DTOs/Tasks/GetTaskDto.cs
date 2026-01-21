using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Application.DTOs.Tasks;

public record GetTaskDto
    (
        string Title,
        string Description,
        DateTime CreateAt,
        DateTime DoneDate,
        string MemberName,
        ushort Percentage,
        string TeamName,
        IEnumerable<Comment>Comments
    );
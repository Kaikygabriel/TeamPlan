using TeamPlan.Domain.BackOffice.Entities;
using Task = TeamPlan.Domain.BackOffice.Entities.Team;

namespace TeamPlan.Application.DTOs.Team;

public record TeamDashBoardDTO
    (
        IEnumerable<string>NameMembers,
        string NameManager,
        IEnumerable<Domain.BackOffice.Entities.Task> Task,
        ushort Percentage,
        IEnumerable<Mark>  Marks
    );
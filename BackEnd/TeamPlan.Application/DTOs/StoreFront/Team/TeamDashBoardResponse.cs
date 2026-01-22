using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Application.DTOs.StoreFront.Team;

public record TeamDashBoardResponse
    (
        Guid? IdOwner,
        Guid ManagerId,
        IEnumerable<Guid> IdMembers,
        IEnumerable<string>NameMembers,
        string NameManager,
        IEnumerable<Domain.BackOffice.Entities.Task> Task,
        ushort Percentage,
        IEnumerable<Mark>  Marks
    );
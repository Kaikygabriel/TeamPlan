using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.ValueObject;

public class Kanban : Entity
{
    public string Title { get; init; }
    public ushort Order { get; init; }

    protected Kanban()
    {
        
    }
    private Kanban(string title,ushort order)
    {
        Title = title;
        Order = order;
    }
    
    public static class Factory
    {
        public static Result<Kanban> Create(string title, ushort order)
        {
            return Result<Kanban>.Success(new(title, order));
        }
    }
}
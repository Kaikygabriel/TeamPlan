namespace TeamPlan.Domain.BackOffice.Commum;

public class QueryPagination
{
    public int Skip { get;private set; }
    public int Take { get;private set; }

    public void AlterPropertyIfInvalid()
    {
        if (Skip < 0)
            Skip = 0;
        if (Take > 50)
            Take = 50;
    }
}
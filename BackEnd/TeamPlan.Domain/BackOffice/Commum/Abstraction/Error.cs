namespace TeamPlan.Domain.BackOffice.Commum.Abstraction;

public class Error
{
    public Error(string title, string menssage)
    {
        Title = title;
        Menssage = menssage;
    }

    public string Title { get; set; }
    public string Menssage { get; set; }
}
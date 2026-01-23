namespace TeamPlan.Domain.BackOffice.Commum;

public class EmailBuilder
{
    public string ToAddress { get;private set; }
    public string Message { get;private set; }
    public string Title { get;private set; }
    public string Name { get;private set; }
    private EmailBuilder()
    {
        
    }

    public static EmailBuilder Configuration() => new EmailBuilder();

    public EmailBuilder To(string to)
    {
        ToAddress = to;
        return this;
    } 
    public EmailBuilder WithName(string name)
    {
        Name = name;
        return this;
    } 
    public EmailBuilder WithMessage(string message)
    {
        Message = message;
        return this;
    } 

    public EmailBuilder WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public EmailBuilder Build()
        => this;
}

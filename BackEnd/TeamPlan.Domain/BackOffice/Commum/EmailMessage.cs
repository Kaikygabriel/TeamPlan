namespace TeamPlan.Domain.BackOffice.Commum;

public class EmailMessage
{
    public static string Welcome(string address) 
        => $"Boas Vindas  {address}!";
    
    public static string CommentMethod(string messageComment) 
        => $"Alguem fez um comentario e citou voce : \n \n {messageComment}  ";
}
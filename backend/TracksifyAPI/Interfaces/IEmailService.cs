namespace TracksifyAPI.Interfaces
{
    public interface IEmailService
    {
        Task SendHtmlEmailAsync(string receiverEmail, 
                                string subject, 
                                string templateFileName, 
                                object model);
    }
}

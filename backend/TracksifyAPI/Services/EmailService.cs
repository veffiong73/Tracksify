using MailKit.Net.Smtp;
using MimeKit;
using TracksifyAPI.Interfaces;


namespace TracksifyAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _templatesFolderPath;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        public EmailService(string templatesFolderPath, ILogger logger, IConfiguration config)
        {
            _templatesFolderPath = templatesFolderPath;
            _logger = logger;
            _config = config;
        }
        public async Task SendHtmlEmailAsync(string receiverEmail, string subject, string templateFileName, object model)
        {
            var templateFileNameWithSuffix = templateFileName + ".html";

            var templatePath = Path.Combine(_templatesFolderPath, templateFileNameWithSuffix);

            System.Console.WriteLine(templatePath);

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Template file not found: {templateFileNameWithSuffix}");
            }

            var templateContent = await File.ReadAllTextAsync(templatePath);
            var mergedContent = MergeTemplateWithModel(templateContent, model);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tracksify", _config.GetSection("EmailSettings:EmailSender").Value));
            message.To.Add(new MailboxAddress("", receiverEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = mergedContent
            };

            message.Body = bodyBuilder.ToMessageBody();



            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(_config.GetSection("EmailSettings:EmailSender").Value, _config.GetSection("EmailSettings:EmailPassword").Value);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            _logger.LogDebug($"{templateFileName} email sent to {receiverEmail}");
        }

        private string MergeTemplateWithModel(string templateContent, object model)
        {
            // Replace placeholders in the template with actual values from the model
            // This can be done in various ways depending on your templating needs
            // For simplicity, this example uses string.Replace
            foreach (var property in model.GetType().GetProperties())
            {
                var placeholder = $"{{{{{property.Name}}}}}";
                var value = property.GetValue(model)?.ToString();
                templateContent = templateContent.Replace(placeholder, value);
            }

            return templateContent;
        }
    }
}
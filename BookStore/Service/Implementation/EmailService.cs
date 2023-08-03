using BookStore.Models;
using BookStore.Service.Interface;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookStore.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;        

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        public async Task SendTestEmail(UserEmailOptionsModel model)
        {
            model.Subject = UpdatePlaceHolders("This is Book Store application",model.PlaceHolders);
            model.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"),model.PlaceHolders);
            await SendEmail(model);
        }

        public async Task SendEmailForEmailConfirmation(UserEmailOptionsModel model)
        {
            model.Subject = UpdatePlaceHolders("Hello {{UserName}}, Confirm your mail id", model.PlaceHolders);
            model.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"), model.PlaceHolders);
            await SendEmail(model);
        }


        public async Task SendEmailForgotPassword(UserEmailOptionsModel model)
        {
            model.Subject = UpdatePlaceHolders("Hello {{UserName}}, reset your password", model.PlaceHolders);
            model.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), model.PlaceHolders);
            await SendEmail(model);
        }



        private async Task SendEmail(UserEmailOptionsModel model)
        {
            MailMessage mail = new MailMessage
            {
                Subject = model.Subject,
                Body = model.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML,

            };
            foreach (var item in model.ToEmails)
            {
                mail.To.Add(item);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSsl,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential,
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
        private string UpdatePlaceHolders(string text, List<KeyValuePair<string,string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text)&&keyValuePairs!=null)
            {
                foreach (var item in keyValuePairs)
                {
                    if (text.Contains(item.Key))
                    {
                        text = text.Replace(item.Key,item.Value);
                    }
                }
            }
            return text;
        }
    }
}

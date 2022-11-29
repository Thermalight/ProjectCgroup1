using System.Net.Mail;
using ChengetaWebApp.Api.Database.Models;
using ChengetaWebApp.Api.Database;

namespace ChengetaWebApp.Api.Services;

public class EmailService
{
    private readonly DatabaseService _databaseService;

    public EmailService(DatabaseService databaseService) 
    {
        _databaseService = databaseService;
    }

    public void MailSubscribers(Notification notification) 
    {
        var context = SqliteDbContext.Create();

        if (!context.Subscribers.Any())
            return;

        SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
        var mail = new MailMessage();
        mail.From = new MailAddress("chengeta_guns@hotmail.com");

        foreach (var subscriber in context.Subscribers)
        {
            mail.To.Add(subscriber.User.Email);
        }

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0);
        epoch = epoch.AddSeconds(notification.Time).ToLocalTime();
        var dt = epoch.ToString("HH:mm");

        mail.Subject = $"{notification.SoundType} detection - {dt}";
        mail.IsBodyHtml = true;
        string htmlBody;
        htmlBody = @$"
            <h1>{notification.SoundType}</h1>
            <p>A {notification.SoundType} has been detected at<br>latutude: {notification.Latitude}<br>longitude: {notification.Longitude}</p>
            <a href='https://www.chengeta.xyz'>Go to dashboard</a><br>
            <img src='https://ichef.bbci.co.uk/images/ic/976xn/p08h0lkd.jpg' style = 'width: 500px'>
        ";
        mail.Body = htmlBody;
        SmtpServer.Port = 587;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Credentials = new System.Net.NetworkCredential("chengeta_guns@hotmail.com", "PASSWORD!@#$%");
        SmtpServer.EnableSsl = true;
        SmtpServer.Send(mail);
    }
}
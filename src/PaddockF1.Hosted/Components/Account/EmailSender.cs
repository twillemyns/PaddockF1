using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;
using PaddockF1.Hosted.Data;

namespace PaddockF1.Hosted.Components.Account;

public class EmailSender : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
        SendEmailAsync(email, "Confirm your email",
            $"Veuillez confirmer votre compte en cliquant sur ce lien: <a href='{confirmationLink}'>lien</a>");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        SendEmailAsync(email, "Reset your password",
            $"Veuillez réinitialiser votre mot de passe en cliquant sur ce lien: <a href='{resetLink}'>lien</a>");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
        SendEmailAsync(email, "Reset your password",
            $"Veuillez réinitialiser votre mot de passe en entrant ce code: {resetCode}.");

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        // if (string.IsNullOrEmpty(Options.EmailAuthKey))
        // {
        //     throw new Exception("Null or empty email auth key");
        // }

        await Execute(subject, message, toEmail);
    }

    private async Task Execute(string subject, string message, string toEmail)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("cathryn.graham@ethereal.email"));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = message
        };

        // Envoi via SMTP avec MailKit et Ethereal (serveur de test)
        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync("cathryn.graham@ethereal.email", "sPbbrkp8JzmZbMkNNj");
        await client.SendAsync(email);
        await client.DisconnectAsync(true);

        // Envoi via SMTP avec MailKit et SMTP Gmail
        // using var client = new SmtpClient();
        // await client.ConnectAsync("smtp-relay.gmail.com", 587, SecureSocketOptions.StartTls);
        // await client.AuthenticateAsync("willemynstheo@gmail.com", configuration["AccountPassword"]);
        // await client.SendAsync(email);
        // await client.DisconnectAsync(true);
    }
}
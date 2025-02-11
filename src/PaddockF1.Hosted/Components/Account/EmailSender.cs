using Azure;
using Azure.Communication.Email;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Identity;
using PaddockF1.Hosted.Data.Models;

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
        const string keyVaultUri = "https://dbpwd.vault.azure.net";
        var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
        var secret = await client.GetSecretAsync("smtp-string");
        var connectionString = secret.Value.Value;
        
        var emailClient = new EmailClient(connectionString);

        var email = new EmailMessage(
            senderAddress: "DoNotReply@5e23fcf6-bfcd-4ac1-a03d-d69107ac1538.azurecomm.net",
            content: new EmailContent(subject)
            {
                Html = message
            },
            recipients: new EmailRecipients(new List<EmailAddress> { new(toEmail) })
        );

        await emailClient.SendAsync(WaitUntil.Completed, email);
    }
}
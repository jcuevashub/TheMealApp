using Email.Application.Interfaces.Services;
using Email.Core.Dtos;
using System.Net.Http.Headers;
using System.Text;

namespace Email.Shared.Services;

public class MailgunEmailService : IEmailService
{
    private readonly string? _domain;
    private readonly string? _apiKey;

    public MailgunEmailService(string domain, string apiKey)
    {
        _domain = domain;
        _apiKey = apiKey;
    }

    public async Task SendEmailAsync(EmailMessage message)
    {

        using (var httpClient = new HttpClient())
        {
            var authToken = Encoding.ASCII.GetBytes($"api:{_apiKey}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

            var formContent = new FormUrlEncodedContent(new[]
                   {
                    new KeyValuePair<string, string>("from", message.From),
                    new KeyValuePair<string, string>("to", message.To),
                    new KeyValuePair<string, string>("subject", message.Subject),
                    new KeyValuePair<string, string>("text", message.Body)
                });

            var result = await httpClient.PostAsync($"https://api.mailgun.net/v3/{_domain}/messages", formContent);
            result.EnsureSuccessStatusCode();
        }


    }
}

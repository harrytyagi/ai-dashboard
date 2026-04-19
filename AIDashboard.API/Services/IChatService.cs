namespace AIDashboard.API.Services;

public interface IChatService
{
    Task<string> GetReplyAsync(string userMessage);
}
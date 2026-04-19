using System.Text;
using System.Text.Json;

namespace AIDashboard.API.Services;

public class OllamaChatService : IChatService
{
    private readonly HttpClient _httpClient;

    public OllamaChatService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetReplyAsync(string userMessage)
    {
        var payload = new
        {
            model = "mistral",
            prompt = userMessage,
            stream = false
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:11434/api/generate", content);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JsonElement>(responseBody);
        return result.GetProperty("response").GetString() ?? "No response";
    }
}
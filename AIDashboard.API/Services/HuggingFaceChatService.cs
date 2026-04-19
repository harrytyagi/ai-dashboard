using System.Text;
using System.Text.Json;

namespace AIDashboard.API.Services;

public class HuggingFaceChatService : IChatService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public HuggingFaceChatService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["HuggingFace:ApiKey"]!;
    }

    public async Task<string> GetReplyAsync(string userMessage)
    {
        var payload = new
        {
            model = "meta-llama/Llama-3.1-8B-Instruct",
            messages = new[]
            {
                new { role = "user", content = userMessage }
            },
            max_tokens = 500
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.PostAsync(
            "https://router.huggingface.co/v1/chat/completions",
            content);

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return $"HF Error {response.StatusCode}: {responseBody}";

        var result = JsonSerializer.Deserialize<JsonElement>(responseBody);
        return result.GetProperty("choices")[0]
                     .GetProperty("message")
                     .GetProperty("content")
                     .GetString() ?? "No response";
    }
}
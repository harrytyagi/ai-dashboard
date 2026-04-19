namespace AIDashboard.API.Models;

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
}

public class ChatResponse
{
    public string Reply { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string? Error { get; set; }
}
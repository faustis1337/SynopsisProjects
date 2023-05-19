namespace SenderAPI.Models;

public class MessageEntity
{
    public Dictionary<string, object> Headers { get; } = new();

    public string Id { get; set; }
    public string Message { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models;

public class Message
{
    public Guid Id { get; set; }

    public Guid ConversationId { get; set; }

    [Required]
    public string SenderName { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public Conversation Conversation { get; set; } = null!;
}

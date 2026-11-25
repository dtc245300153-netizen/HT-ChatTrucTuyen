using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models;

public class Conversation
{
    public Guid Id { get; set; }

    [Required]
    public string CustomerName { get; set; } = string.Empty;

    public string? CustomerEmail { get; set; }

    public string? Subject { get; set; }

    public ConversationStatus Status { get; set; } = ConversationStatus.Waiting;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ClosedAt { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

namespace ChatApi.Models;

public enum ConversationStatus
{
    Waiting,
    Active,
    Closed
}

public class Conversation
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public ConversationStatus Status { get; set; } = ConversationStatus.Waiting;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ClosedAt { get; set; }
    public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}

public class Participant
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTimeOffset JoinedAt { get; set; }
    public Conversation Conversation { get; set; } = null!;
}

public class ChatMessage
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderRole { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTimeOffset SentAt { get; set; }
    public Conversation Conversation { get; set; } = null!;
}

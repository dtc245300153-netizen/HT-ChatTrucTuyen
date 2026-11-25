namespace ChatApi.DTOs;

/// <summary>Message returned as part of a conversation's details.</summary>
public sealed record MessageResponse(
    Guid Id,
    Guid ConversationId,
    string SenderName,
    string Content,
    DateTimeOffset SentAt);

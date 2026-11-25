using ChatApi.Models;

namespace ChatApi.DTOs;

/// <summary>Conversation summary returned by the API.</summary>
public sealed record ConversationResponse(
    Guid Id,
    string CustomerName,
    string? CustomerEmail,
    string? Subject,
    ConversationStatus Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ClosedAt);

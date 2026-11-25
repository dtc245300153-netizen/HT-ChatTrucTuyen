using ChatApi.Models;

namespace ChatApi.DTOs;

/// <summary>Conversation details, including its messages.</summary>
public sealed record ConversationDetailsResponse(
    Guid Id,
    string CustomerName,
    string? CustomerEmail,
    string? Subject,
    ConversationStatus Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ClosedAt,
    IReadOnlyList<MessageResponse> Messages);

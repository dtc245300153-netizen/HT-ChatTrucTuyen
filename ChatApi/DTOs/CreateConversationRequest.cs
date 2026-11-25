using System.ComponentModel.DataAnnotations;

namespace ChatApi.DTOs;

/// <summary>Request data for creating a conversation.</summary>
public sealed record CreateConversationRequest
{
    /// <summary>The customer's name.</summary>
    [Required]
    public required string CustomerName { get; init; }

    /// <summary>The customer's email address, if provided.</summary>
    [EmailAddress]
    public string? CustomerEmail { get; init; }

    /// <summary>The subject of the conversation, if provided.</summary>
    public string? Subject { get; init; }
}

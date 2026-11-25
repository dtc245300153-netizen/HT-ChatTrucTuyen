using ChatApi.DTOs;
using ChatApi.Models;

namespace ChatApi.Services;

public interface IConversationService
{
    Task<ConversationResponse> CreateAsync(
        CreateConversationRequest request,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ConversationResponse>> GetAllAsync(
        ConversationStatus? status,
        CancellationToken cancellationToken);

    Task<ConversationDetailsResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}

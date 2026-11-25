using ChatApi.Data;
using ChatApi.DTOs;
using ChatApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Services;

public sealed class ConversationService(AppDbContext dbContext) : IConversationService
{
    public async Task<ConversationResponse> CreateAsync(
        CreateConversationRequest request,
        CancellationToken cancellationToken)
    {
        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            Subject = request.Subject,
            Status = ConversationStatus.Waiting,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Conversations.Add(conversation);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ToResponse(conversation);
    }

    public async Task<IReadOnlyList<ConversationResponse>> GetAllAsync(
        ConversationStatus? status,
        CancellationToken cancellationToken)
    {
        var query = dbContext.Conversations.AsNoTracking();
        if (status.HasValue)
        {
            query = query.Where(conversation => conversation.Status == status.Value);
        }

        var conversations = await query
            .OrderByDescending(conversation => conversation.CreatedAt)
            .ToListAsync(cancellationToken);

        return conversations.Select(ToResponse).ToList();
    }

    public async Task<ConversationDetailsResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var conversation = await dbContext.Conversations
            .AsNoTracking()
            .Include(item => item.Messages)
            .SingleOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (conversation is null)
        {
            return null;
        }

        return new ConversationDetailsResponse(
            conversation.Id,
            conversation.CustomerName,
            conversation.CustomerEmail,
            conversation.Subject,
            conversation.Status,
            ToUtcOffset(conversation.CreatedAt),
            conversation.ClosedAt.HasValue ? ToUtcOffset(conversation.ClosedAt.Value) : null,
            conversation.Messages
                .OrderBy(message => message.SentAt)
                .Select(message => new MessageResponse(
                    message.Id,
                    message.ConversationId,
                    message.SenderName,
                    message.Content,
                    ToUtcOffset(message.SentAt)))
                .ToList());
    }

    private static ConversationResponse ToResponse(Conversation conversation) =>
        new(
            conversation.Id,
            conversation.CustomerName,
            conversation.CustomerEmail,
            conversation.Subject,
            conversation.Status,
            ToUtcOffset(conversation.CreatedAt),
            conversation.ClosedAt.HasValue ? ToUtcOffset(conversation.ClosedAt.Value) : null);

    private static DateTimeOffset ToUtcOffset(DateTime value) =>
        new(DateTime.SpecifyKind(value, DateTimeKind.Utc));
}

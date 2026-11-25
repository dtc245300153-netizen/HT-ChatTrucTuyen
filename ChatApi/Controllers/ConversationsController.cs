using ChatApi.DTOs;
using ChatApi.Models;
using ChatApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;

[ApiController]
[Route("conversations")]
public sealed class ConversationsController(IConversationService conversationService) : ControllerBase
{
    /// <summary>Creates a new conversation in the waiting state.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ConversationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConversationResponse>> Create(
        [FromBody] CreateConversationRequest request,
        CancellationToken cancellationToken)
    {
        var conversation = await conversationService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = conversation.Id }, conversation);
    }

    /// <summary>Gets conversations, optionally filtered by status.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ConversationResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ConversationResponse>>> GetAll(
        [FromQuery] ConversationStatus? status,
        CancellationToken cancellationToken)
    {
        var conversations = await conversationService.GetAllAsync(status, cancellationToken);
        return Ok(conversations);
    }

    /// <summary>Gets a conversation and its messages by ID.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ConversationDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConversationDetailsResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var conversation = await conversationService.GetByIdAsync(id, cancellationToken);
        return conversation is null ? NotFound() : Ok(conversation);
    }
}

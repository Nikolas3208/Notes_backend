using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.API.Contracts;
using Notes.API.Contracts.Note;
using Notes.Core.Abstractions;
using Notes.Core.Models;

namespace Notes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NoteController : ControllerBase
{
    private readonly INotesService _notesService;
    
    public NoteController(INotesService notesService)
    {
        _notesService = notesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<NoteResponce>>> Get()
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        var notes = await _notesService.Get(userId);

        var notesResponce = notes
            .Select(n => new NoteResponce(n.Id, n.OwnerId, n.Title, n.Text, n.Created))
            .ToList();

        return Ok(notesResponce);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] NoteRequest noteRequest)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        var (error, note) = Note.Create(
            Guid.NewGuid(),
            userId,
            noteRequest.Title,
            noteRequest.Text,
            DateTime.UtcNow);

        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        await _notesService.Create(note);

        return note.Id;
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] NoteRequest request)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        var noteId = await _notesService.Update(id, userId, request.Title, request.Text);

        return Ok(noteId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        var noteId = await _notesService.Delete(id, userId);

        return Ok(id);
    }
}
using Microsoft.AspNetCore.Mvc;
using Notes.API.Contracts;
using Notes.Core.Abstractions;
using Notes.Core.Models;

namespace Notes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var notes = await _notesService.Get(new Guid("79d598f8-e927-4335-b37b-b062c0267118"));

        var notesResponce = notes
            .Select(n => new NoteResponce(n.Id, n.OwnerId, n.Title, n.Text, n.Created))
            .ToList();

        return Ok(notesResponce);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] NoteRequest noteRequest)
    {
        var (error, note) = Note.Create(
            Guid.NewGuid(),
            new Guid("79d598f8-e927-4335-b37b-b062c0267118"),
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
        var ownerId = new Guid("79d598f8-e927-4335-b37b-b062c0267118");
        
        var noteId = await _notesService.Update(id, ownerId, request.Title, request.Text);

        return Ok(noteId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
        var ownerId = new Guid("79d598f8-e927-4335-b37b-b062c0267118");
        
        var noteId = await _notesService.Delete(id, ownerId);

        return Ok(id);
    }
}
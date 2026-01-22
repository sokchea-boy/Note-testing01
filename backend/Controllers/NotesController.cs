using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotesController : ControllerBase
{
    private readonly IDbConnection _db;

    public NotesController(IDbConnection db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotes(string? search = null, string? sortBy = "createdAt", string? sortOrder = "desc")
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var query = "SELECT * FROM Notes WHERE UserId = @UserId";
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);

        if (!string.IsNullOrEmpty(search))
        {
            query += " AND (Title LIKE @Search OR Content LIKE @Search)";
            parameters.Add("@Search", $"%{search}%");
        }

        query += $" ORDER BY {sortBy} {sortOrder}";

        var notes = await _db.QueryAsync<Note>(query, parameters);
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNote(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var note = await _db.QueryFirstOrDefaultAsync<Note>("SELECT * FROM Notes WHERE Id = @Id AND UserId = @UserId", new { Id = id, UserId = userId });
        if (note == null) return NotFound();
        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] Note note)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        note.CreatedAt = DateTime.UtcNow;
        note.UpdatedAt = DateTime.UtcNow;
        note.UserId = userId;

        var sql = "INSERT INTO Notes (Title, Content, CreatedAt, UpdatedAt, UserId) VALUES (@Title, @Content, @CreatedAt, @UpdatedAt, @UserId); SELECT last_insert_rowid();";
        var id = await _db.ExecuteScalarAsync<int>(sql, note);
        note.Id = id;
        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, [FromBody] Note note)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        note.Id = id;
        note.UpdatedAt = DateTime.UtcNow;

        var sql = "UPDATE Notes SET Title = @Title, Content = @Content, UpdatedAt = @UpdatedAt WHERE Id = @Id AND UserId = @UserId";
        var affectedRows = await _db.ExecuteAsync(sql, new { note.Title, note.Content, note.UpdatedAt, Id = id, UserId = userId });
        if (affectedRows == 0) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var affectedRows = await _db.ExecuteAsync("DELETE FROM Notes WHERE Id = @Id AND UserId = @UserId", new { Id = id, UserId = userId });
        if (affectedRows == 0) return NotFound();
        return NoContent();
    }
}
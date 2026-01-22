using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Data;
using Dapper;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var query = "SELECT * FROM Notes WHERE 1=1";
        var parameters = new DynamicParameters();

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
        var note = await _db.QueryFirstOrDefaultAsync<Note>("SELECT * FROM Notes WHERE Id = @Id", new { Id = id });
        if (note == null) return NotFound();
        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] Note note)
    {
        note.CreatedAt = DateTime.UtcNow;
        note.UpdatedAt = DateTime.UtcNow;

        var sql = "INSERT INTO Notes (Title, Content, CreatedAt, UpdatedAt, UserId) VALUES (@Title, @Content, @CreatedAt, @UpdatedAt, @UserId); SELECT last_insert_rowid();";
        var id = await _db.ExecuteScalarAsync<int>(sql, note);
        note.Id = id;
        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, [FromBody] Note note)
    {
        note.Id = id;
        note.UpdatedAt = DateTime.UtcNow;

        var sql = "UPDATE Notes SET Title = @Title, Content = @Content, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        var affectedRows = await _db.ExecuteAsync(sql, note);
        if (affectedRows == 0) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var affectedRows = await _db.ExecuteAsync("DELETE FROM Notes WHERE Id = @Id", new { Id = id });
        if (affectedRows == 0) return NotFound();
        return NoContent();
    }
}
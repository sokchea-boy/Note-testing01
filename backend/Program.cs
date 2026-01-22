using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// =====================
// SERVICES
// =====================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================
// DATABASE (SQLite + Dapper)
// =====================
var connectionString = "Data Source=notes.db";

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqliteConnection(connectionString));

// Initialize database
using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();

    var sql = @"
    CREATE TABLE IF NOT EXISTS Users (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Email TEXT UNIQUE NOT NULL,
        PasswordHash TEXT NOT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
    );

    CREATE TABLE IF NOT EXISTS Notes (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Title TEXT NOT NULL,
        Content TEXT,
        CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
        UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
        UserId INTEGER,
        FOREIGN KEY (UserId) REFERENCES Users(Id)
    );";

    connection.Execute(sql);
}

// =====================
// CORS (FIXED)
// =====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5175") // 👈 MUST MATCH VUE PORT
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// =====================
// MIDDLEWARE
// =====================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔥 CORS MUST BE HERE (before MapControllers)
app.UseCors("AllowVueFrontend");

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

// Add request logging for debugging
app.Use(async (context, next) =>
{
    Console.WriteLine($"{DateTime.Now}: {context.Request.Method} {context.Request.Path} - {context.Response.StatusCode}");
    await next();
});

app.Run();


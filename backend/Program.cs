using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================
// SERVICES
// =====================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "backend",
            ValidAudience = "frontend",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-for-jwt"))
        };
    });
builder.Services.AddAuthorization();

// =====================
// DATABASE (SQLite + Dapper)
// =====================
var databasePath = Path.Combine(builder.Environment.ContentRootPath, "notes.db");
var connectionString = $"Data Source={databasePath}";

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqliteConnection(connectionString));

// Initialize database
Console.WriteLine($"Initializing database with connection string: {connectionString}");
using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Database connection opened successfully");

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
    Console.WriteLine("Database tables created or already exist");
}

// =====================
// CORS (FIXED)
// =====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://localhost:5175") // 👈 ALLOW DEV AND PROD PORTS
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

app.UseAuthentication();
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


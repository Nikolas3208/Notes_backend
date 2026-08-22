using Microsoft.EntityFrameworkCore;
using Notes.Application.Services;
using Notes.Core.Abstractions;
using Notes.DataAccess;
using Notes.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(NotesDbContext))));

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<INotesRepository, NotesRepository>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<INotesService, NotesService>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

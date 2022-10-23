using LogInAPI.Models;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LogInBDContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("LogIn"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", async (LogInBDContext context) => await context.Users.ToListAsync());

app.MapPost("/", async (User user, LogInBDContext context) =>
{

    await context.Users.AddAsync(
        new User
        {
            Name = $"{user.Name}",
            OtherName = $"{user.OtherName}",
            LastName = $"{user.LastName}",
            OtherLastName = $"{user.OtherLastName}",
            Email = $"{user.Email}"
        });

    await context.SaveChangesAsync();

    return Results.Created($"/{user.Email}", user);

});


app.MapPut("/", async (int id, User userUpdate, LogInBDContext context) =>
{
    if (await context.Users.FindAsync(id) is User user)
    {
        user.Name = userUpdate.Name;
        user.OtherName = userUpdate.OtherName;
        user.LastName = userUpdate.LastName;
        user.OtherLastName = userUpdate.OtherLastName;
        user.Email = userUpdate.Email;

        await context.SaveChangesAsync();

        return Results.NoContent();
    }

    return Results.NotFound();
});

app.MapDelete("/", async (int id, LogInBDContext context) =>
{
    if (await context.Users.FindAsync(id) is User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();
});

app.Run();

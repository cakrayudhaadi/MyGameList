using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyGameList.Src.Features;
using MyGameList.Src.Features.Categories;
using MyGameList.Src.Features.Characters;
using MyGameList.Src.Features.GameMakers;
using MyGameList.Src.Features.Games;
using MyGameList.Src.Features.Users;
using MyGameList.Src.Shared.Security;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyGameListDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<MyGameListDbContext>()
    .AddDefaultTokenProviders();

// Add application services
builder.Services.AddCategoryServices();
builder.Services.AddGameMakerServices();
builder.Services.AddGameServices();
builder.Services.AddUserServices();
builder.Services.AddCharacterServices();
builder.Services.AddSecurityServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

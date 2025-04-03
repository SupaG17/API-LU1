using System.Data.Common;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LU1_project.Repositories;
using LU2_project.Models;


var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration["SqlConnectionString"];
if (string.IsNullOrWhiteSpace(sqlConnectionString))
    throw new InvalidProgramException("Configuration variable SqlConnectionString not found");

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedPhoneNumber = false;

        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
    })
    .AddDapperStores(options => { options.ConnectionString = sqlConnectionString; });
SqlRepository sqlRepository = new SqlRepository(sqlConnectionString);
UserInfo userInfo = new("SimonTest", "Test123", 2, 2);
await sqlRepository.InsertAsync(userInfo);

builder.Services
    .AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme)
    .Configure(options =>
    {
        options.BearerTokenExpiration = TimeSpan.FromMinutes(60);
    });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseAuthorization();

app.MapGroup("/account").MapIdentityApi<IdentityUser>();

app.MapPost("/account/logout",
    async (SignInManager<IdentityUser> signInManager, [FromBody] object empty) =>
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    })
    .RequireAuthorization();

app.MapControllers().RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


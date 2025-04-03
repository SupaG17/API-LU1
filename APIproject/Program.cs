using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LU1_project.Repositories; 
using LU1_project.Interfaces;
using LU1_project.UserUpdateService;

var builder = WebApplication.CreateBuilder(args);

// Get Connection String from Configuration
var sqlConnectionString = builder.Configuration["SqlConnectionString"];
if (string.IsNullOrWhiteSpace(sqlConnectionString))
    throw new InvalidProgramException("Configuration variable SqlConnectionString not found");

// ? Register IDbConnection (Fixes Dependency Injection issue)
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(sqlConnectionString));
builder.Services.AddScoped<ISqlRepository, SqlRepository>();
builder.Services.AddScoped<UserUpdateService>();

// Add Authorization & Identity
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

builder.Services
    .AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme)
    .Configure(options =>
    {
        options.BearerTokenExpiration = TimeSpan.FromMinutes(60);
    });

builder.Services.AddControllers();
builder.Services.AddOpenApi(); // OpenAPI Documentation

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

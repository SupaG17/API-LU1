using System.Data.Common;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>;
    .AddDapperStores(options => { options.ConnectionString = DbConnectionString;
    .AddIdentityApiEndpoints<IdentityUser>(options =>
     {
         options.User.RequireUniqueEmail = true;
         options.SignIn.RequireConfirmedPhoneNumber = true;

         options.Password.RequireDigit = false;
         options.Password.RequireLowercase = true;
         options.Password.RequireUppercase = true;
     });
builder.Services
    .AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme)
    .Configure(options => {
        options.BearerTokenExpiration = TimeSpan.FromMinutes(value: 60);
    });


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseAuthorization();
app.MapGroup(prefix: "/account")
.MapIdentityApi<IdentityUser>();
     app.MapPost(pattern: "/account/logout",
         async (SignInManager<IdentityUser> signInManager,
         [FromBody] object empty) => {
             if (empty != null)
             {
                 await signInManager.SignOutAsync();
                 return Results.Ok();
             }
             return Results.Unauthorized();
         })
         .RequireAuthorization();
app.MapControllers()
   .RequireAuthorization();

     // Configure the HTTP request pipeline.
     if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

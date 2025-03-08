using System.Text;
using jwtAspNet;
using jwtAspNet.Models;
using jwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", p => p.RequireRole("admin"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (TokenService service) =>
{
    var user = new User(
        Id: 1,
        Name: "John Doe",
        Email: "john.doe@gmail.com",
        Image: "https://picsum.photos/200",
        Password: "SuperSecure1",
        Roles: ["user"]
     );

    return service.Create(user);
});

app.MapGet("/restrict", () => "You have Access!").RequireAuthorization();
app.MapGet("/admin", () => "Welcome, Admin! You have full access.").RequireAuthorization("Admin");


app.Run();

using jwtAspNet.Models;
using jwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();
builder.Services.AddAuthentication();

var app = builder.Build();
app.UseAuthentication();

app.MapGet("/", (TokenService service) =>
{
    var user = new User(
        Id: 1,
        Name: "John Doe",
        Email: "john.doe@gmail.com",
        Image: "https://picsum.photos/200",
        Password: "SuperSecure1",
        Roles: ["admin", "user"]
     );

    service.Create(user);
});

app.Run();

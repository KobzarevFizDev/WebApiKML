using WebApiKML.Services;

var builder = WebApplication.CreateBuilder(args);

int port = builder.Configuration.GetValue<int>("PORT");

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(port);
});

builder.Services.AddControllers();

builder.Services.AddTransient<KMLService>();

var app = builder.Build();

app.MapControllers();

app.Run();

var builder = WebApplication.CreateBuilder(args);
// Add Services


builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(optn =>
{
    optn.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure Request Pipeline
app.MapCarter();

app.Run();

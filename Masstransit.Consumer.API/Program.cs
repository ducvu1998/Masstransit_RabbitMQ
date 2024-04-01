using Masstransit.Consumer.API.DependencyInjection.Extentions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//add serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Logging.ClearProviders().AddSerilog();
builder.Host.UseSerilog();

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .WriteTo.Console()
//    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//add masstransit
builder.Services.AddConfigurationMasstansitRabbitMQ(builder.Configuration);

//AddMediatr
builder.Services.AddMediatr();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch(Exception ex)
{
    Log.Fatal(ex, "An unhandle exception occured during boostrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}
//app.Run();

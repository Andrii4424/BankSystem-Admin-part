using Serilog;
using WebUI.Handlers;
using WebUI.StartupServicesInjection;

var builder = WebApplication.CreateBuilder(args);


//Logging
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});


AddApplicationServices.AddServices(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandlerMiddleware();
}

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();

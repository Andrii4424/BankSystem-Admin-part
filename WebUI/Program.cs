using WebUI.Handlers;
using WebUI.StartupServicesInjection;

var builder = WebApplication.CreateBuilder(args);


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

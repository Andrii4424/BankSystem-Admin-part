using WebUI.StartupServicesInjection;

var builder = WebApplication.CreateBuilder(args);


AddApplicationServices.AddServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();

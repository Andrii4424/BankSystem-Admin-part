using WebUI.StartupServicesInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var app = builder.Build();

AddApplicationServices.AddServices(builder.Services, builder.Configuration);

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();

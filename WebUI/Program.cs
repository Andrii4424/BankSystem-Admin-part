using Microsoft.AspNetCore.Localization;
using Serilog;
using System.Globalization;
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

//Localization
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-GB"),
        new CultureInfo("uk-UA")
    };
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
    options.DefaultRequestCulture = new RequestCulture("en-GB");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

AddApplicationServices.AddServices(builder.Services, builder.Configuration);

var app = builder.Build();
app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
}
else
{
    app.UseHsts();
    app.UseExceptionHandlerMiddleware();
}

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();

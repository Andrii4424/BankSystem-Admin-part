using Application.Services.BankServices;
using Domain.RepositoryContracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace WebUI.StartupServicesInjection
{
    public static class AddApplicationServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BankAppContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }
                );

            //Services

            services.AddScoped<IBankRepository, BankReadService>();

            return services;
        }
    }
}

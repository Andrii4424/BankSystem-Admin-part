using Application.DTO;
using Application.ServiceContracts.BankServiceContracts;
using Application.ServiceContracts.ICardTarrifsService;
using Application.Services.BankService;
using Application.Services.BankServices;
using Application.Services.CardTarrifsService;
using Domain.RepositoryContracts;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebUI.StartupServicesInjection
{
    public static class AddApplicationServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BankAppContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }
                );

            //AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IBankRepository), typeof(BankRepository));
            services.AddScoped(typeof(ICardTarrifsRepository), typeof(CardTarrifsRepository));

            //Services
            //Bank services
            services.AddScoped<IBankReadService, BankReadService>();
            services.AddScoped<IBankAddService, BankAddService>();
            services.AddScoped<IBankUpdateService, BankUpdateService>();
            services.AddScoped<IBankDeleteService, BankDeleteService>();

            //Card Tariffs services
            services.AddScoped<ICardTarrifsReadService, CardTarrifsReadService>();
            services.AddScoped<ICardTarrifsAddService, CardTarrifsAddService>();
            services.AddScoped<ICardTarrifsUpdateService, CardTarrifsUpdateService>();
            services.AddScoped<ICardTarrifsDeleteService, CardTarrifsDeleteService>();

            return services;
        }
    }
}

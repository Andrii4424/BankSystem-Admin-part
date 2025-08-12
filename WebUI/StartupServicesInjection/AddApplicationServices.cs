using Application.DTO;
using Application.ServiceContracts.BankServiceContracts;
using Application.ServiceContracts.ICardTarrifsService;
using Application.ServiceContracts.IUserService;
using Application.Services.BankService;
using Application.Services.BankServices;
using Application.Services.CardTarrifsService;
using Application.Services.UserService;
using Domain.RepositoryContracts;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace WebUI.StartupServicesInjection
{
    public static class AddApplicationServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            //Db context
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
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

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

            //User repository
            services.AddScoped<IUserReadService, UserReadService>();
            services.AddScoped<IUserAddService, UserAddService>();
            services.AddScoped<IUserUpdateService, UserUpdateService>();
            services.AddScoped<IUserDeleteService, UserDeleteService>();



            return services;
        }
    }
}

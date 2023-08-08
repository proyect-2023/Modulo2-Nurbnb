using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Nurbnb.Pagos.Application;
using Nurbnb.Pagos.Infrastructure.EF.Contexts;
using Restaurant.SharedKernel.Core;
using Nurbnb.Pagos.Domain.Repositories;
using Nurbnb.Pagos.Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Nurbnb.Pagos.Infrastructure.EF;

namespace Nurbnb.Pagos.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration, bool isDevelopment)
        {
            services.AddApplication();
           services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            AddDatabase(services, configuration, isDevelopment);

            return services;
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            var connectionString =
                    configuration.GetConnectionString("NurbnbDbConnectionString");
            services.AddDbContext<ReadDbContext>(context =>
                    context.UseSqlite(connectionString));
            services.AddDbContext<WriteDbContext>(context =>
                context.UseSqlite(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICatalogoRepository, CatalogoRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();
            services.AddScoped<IDevolucionRepository, DevolucionRepository>();
            services.AddScoped<ICatalogoDevolucionRepository, CatalogoDevolucionRepository>();
            services.AddScoped<IMedioPagoRepository, MedioPagoRepository>();


            using var scope = services.BuildServiceProvider().CreateScope();
            if (!isDevelopment)
            {
                var context = scope.ServiceProvider.GetRequiredService<ReadDbContext>();
                context.Database.Migrate();
            }
        }
    }
}

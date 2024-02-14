using ECommerceApplication.Application.Contracts;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Infrastructure.Repositories;
using ECommerceApplication.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApplication.Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastrutureToDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ECommerceApplicationContext>(
                options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                    ("ECommerceApplicationConnection"),
                    builder =>
                    builder.MigrationsAssembly(
                        typeof(ECommerceApplicationContext)
                        .Assembly.FullName)));
            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository> ();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            return services;
        }
    }
}

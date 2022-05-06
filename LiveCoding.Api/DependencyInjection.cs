using LiveCoding.Persistence;
using LiveCoding.Services;

namespace LiveCoding.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection InitializeDependencyInjection(this IServiceCollection services)
        {
            return services.AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<CashflowService>();
        }
    }
}

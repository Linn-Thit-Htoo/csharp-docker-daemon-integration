using Microsoft.EntityFrameworkCore;
using MigrationCore.AppDbContextModels;
using MigrationCore.Services;

namespace MigrationCore.Extensions;

public static class DependencyInjectionExtensions
{ 
    public static IServiceCollection AddDependencies(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();
        builder.Services.AddHostedService<MigrationHostedService>();

        return services;
    }
}

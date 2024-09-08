using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        #region Data - Infrastructure services

        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseNpgsql(connectionString));

        #endregion
        
        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        #region Use Data - Infrastructure services

        app.UseMigration<CatalogDbContext>();

        #endregion
        
        return app;
    }
}
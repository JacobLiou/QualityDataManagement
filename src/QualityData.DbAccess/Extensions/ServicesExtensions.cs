using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Console = System.Console;

namespace QualityData.DbAccess.Extensions;

/// <summary>
/// EFCore ORM 注入服务扩展类
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// 注入 EFCore 数据服务类
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddEFCoreDataAccessServices(this IServiceCollection services)
    {
        services.AddDbContextFactory<QualityDataContext>((provider, options) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connString = configuration.GetConnectionString("ba");
            options.UseSqlite(connString);
#if DEBUG
            options.LogTo(Console.WriteLine);
#endif
        }, ServiceLifetime.Singleton);


        return services;
    }
}
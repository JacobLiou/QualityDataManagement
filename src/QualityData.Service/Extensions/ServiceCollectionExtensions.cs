using BootstrapBlazor.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QualityData.Service.IServices;
using QualityData.Service.Services;
using ICacheManager = QualityData.Service.IServices.ICacheManager;

namespace QualityData.Service.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注入 ICacheManager 服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddQualityDataServices(this IServiceCollection services)
    {
        // 增加缓存服务
        services.AddMemoryCache();
        services.TryAddSingleton<ICacheManager>(provider =>
        {
            var cache = provider.GetRequiredService<IMemoryCache>();
            var cacheManager = new DefaultCacheManager(cache);
            CacheManager.Init(cacheManager);
            return cacheManager;
        });

        // 增加数据服务
        services.AddSingleton(typeof(IDataService<>), typeof(DefaultDataService<>));

        services.AddSingleton<INavigation, NavigationService>();
        services.AddSingleton<IDict, DictService>();
        services.AddSingleton<IUser, UserService>();
        services.AddSingleton<IRole, RoleService>();
        services.AddSingleton<IGroup, GroupService>();
        services.AddSingleton<ILogin, LoginService>();
        services.AddSingleton<ITrace, TraceService>();
        services.AddSingleton<IApp, AppService>();
        services.AddSingleton<IException, ExceptionService>();

        return services;
    }
}
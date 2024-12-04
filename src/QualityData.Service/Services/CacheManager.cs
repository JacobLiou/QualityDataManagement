using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using QualityData.Service.IServices;
using System.Diagnostics.CodeAnalysis;

namespace QualityData.Service.Services;

/// <summary>
/// 缓存管理类
/// </summary>
public static class CacheManager
{
    [NotNull]
    private static ICacheManager? Cache { get; set; }

    /// <summary>
    /// 由服务调用
    /// </summary>
    /// <param name="cache"></param>
    internal static void Init(ICacheManager cache) => Cache = cache;

    /// <summary>
    /// 获得或者新建数据
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="key"></param>
    /// <param name="valueFactory"></param>
    /// <returns></returns>
    public static TItem GetOrAdd<TItem>(string key, Func<ICacheEntry, TItem> valueFactory) => Cache.GetOrAdd(key, valueFactory);

    /// <summary>
    /// 清除指定键值缓存项
    /// </summary>
    /// <param name="key"></param>
    public static void Clear(string? key = null) => Cache.Clear(key);
}

internal class DefaultCacheManager : ICacheManager
{
    [NotNull]
    private IMemoryCache? Cache { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DefaultCacheManager(IMemoryCache cache) => Cache = cache;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    public T GetOrAdd<T>(string key, Func<ICacheEntry, T> factory) => Cache.GetOrCreate(key, entry =>
    {
        HandlerEntry(key, entry);
        return factory(entry);
    })!;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    public Task<T> GetOrAddAsync<T>(string key, Func<ICacheEntry, Task<T>> factory) => Cache.GetOrCreate(key, entry =>
    {
        HandlerEntry(key, entry);
        return factory(entry);
    })!;

    private static void HandlerEntry(string key, ICacheEntry entry, IChangeToken? token = null)
    {
        if (token != null)
        {
            entry.AddExpirationToken(token);
        }

        // 内置缓存策略 缓存相对时间 10 分钟
        if (entry.AbsoluteExpiration == null && entry.SlidingExpiration == null && !entry.ExpirationTokens.Any())
        {
#if DEBUG
            entry.SlidingExpiration = TimeSpan.FromMilliseconds(100);
#else
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
#endif
        }
        entry.RegisterPostEvictionCallback((key, value, reason, state) =>
        {
        });
    }

    public void Clear(string? key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            Cache.Remove(key);
        }
        else if (Cache is MemoryCache c)
        {
            c.Compact(100);
        }
    }
}
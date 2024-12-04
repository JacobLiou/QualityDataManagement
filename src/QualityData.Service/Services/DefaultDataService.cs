using BootstrapBlazor.Components;
using Microsoft.EntityFrameworkCore;
using QualityData.DbAccess;

namespace QualityData.Service.Services;

/// <summary>
/// EFCore ORM 的 IDataService 接口实现
/// </summary>
internal class DefaultDataService<TModel>(IDbContextFactory<QualityDataContext> factory) : DataServiceBase<TModel> where TModel : class, new()
{
    /// <summary>
    /// 删除方法
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync(IEnumerable<TModel> models)
    {
        // 通过模型获取主键列数据
        // 支持批量删除
        using var context = factory.CreateDbContext();
        context.RemoveRange(models);
        return await context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 保存方法
    /// </summary>
    /// <param name="model"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    public override async Task<bool> SaveAsync(TModel model, ItemChangedType changedType)
    {
        using var context = factory.CreateDbContext();
        if (changedType == ItemChangedType.Add)
        {
            context.Entry(model).State = EntityState.Added;
        }
        else
        {
            context.Entry(model).State = EntityState.Modified;
        }
        return await context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 查询方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
    {
        using var context = factory.CreateDbContext();
        var ret = new QueryData<TModel>()
        {
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true
        };

        var filter = option.ToFilter();
        if (option.IsPage)
        {
            var items = context.Set<TModel>()
                               .Where(filter.GetFilterLambda<TModel>(), filter.HasFilters())
                               .Sort(option.SortName!, option.SortOrder, !string.IsNullOrEmpty(option.SortName))
                               .Count(out var count)
                               .Page((option.PageIndex - 1) * option.PageItems, option.PageItems);

            ret.TotalCount = count;
            ret.Items = items;
        }
        else
        {
            var items = context.Set<TModel>()
                               .Where(filter.GetFilterLambda<TModel>(), filter.HasFilters())
                               .Sort(option.SortName!, option.SortOrder, !string.IsNullOrEmpty(option.SortName))
                               .Count(out var count);
            ret.TotalCount = count;
            ret.Items = items;
        }
        return Task.FromResult(ret);
    }
}
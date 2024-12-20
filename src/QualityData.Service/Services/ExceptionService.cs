﻿using Microsoft.EntityFrameworkCore;
using QualityData.DbAccess;
using QualityData.DbAccess.Models;
using QualityData.Service.IServices;

namespace QualityData.Service.Services;

internal class ExceptionService(IDbContextFactory<QualityDataContext> dbFactory) : IException
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="searchText"></param>
    /// <param name="filter"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageItems"></param>
    /// <param name="sortList"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public (IEnumerable<Error> Items, int ItemsCount) GetAll(string? searchText, ExceptionFilter filter, int pageIndex, int pageItems, List<string> sortList)
    {
        using var context = dbFactory.CreateDbContext();

        var items = context.Set<Error>();

        if (!string.IsNullOrEmpty(searchText))
        {
            items.Where(s => s.Message!.Contains(searchText) || s.StackTrace!.Contains(searchText) || s.ErrorPage!.Contains(searchText));
        }

        if (!string.IsNullOrEmpty(filter.Category))
        {
            items.Where(s => s.Category!.Contains(filter.Category));
        }

        if (!string.IsNullOrEmpty(filter.UserId))
        {
            items.Where(s => s.UserId!.Contains(filter.UserId));
        }

        if (!string.IsNullOrEmpty(filter.ErrorPage))
        {
            items.Where(s => s.ErrorPage!.Contains(filter.ErrorPage));
        }

        items.Where(s => s.LogTime >= filter.Star && s.LogTime <= filter.End);

        if (sortList.Any())
        {
            items.Sort(sortList);
        }
        else
        {
            items.OrderByDescending(s => s.LogTime);
        }

        var data = items.Take(pageItems).Skip(pageItems * (pageIndex - 1)).AsNoTracking().ToList();
        return (data, items.Count());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="exception"></param>
    public bool Log(Error exception)
    {
        using var context = dbFactory.CreateDbContext();
        context.Add(exception);
        return context.SaveChanges() > 0;
    }
}
﻿using Microsoft.EntityFrameworkCore;
using QualityData.DbAccess;
using QualityData.DbAccess.Models;
using QualityData.Service.IServices;

namespace QualityData.Service.Services;

internal class AppService(IDbContextFactory<QualityDataContext> dbFactory) : IApp
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="roleId"></param>
    public List<string> GetAppsByRoleId(string? roleId)
    {
        using var context = dbFactory.CreateDbContext();
        return context.RoleApp.Where(s => s.RoleID == roleId).Select(s => s.AppID!).AsNoTracking().ToList();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="appIds"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool SaveAppsByRoleId(string? roleId, IEnumerable<string> appIds)
    {
        var ret = false;
        try
        {
            using var context = dbFactory.CreateDbContext();
            context.Database.ExecuteSqlRaw("delete from RoleApp where RoleID = {0}", roleId!);
            context.AddRange(appIds.Select(g => new RoleApp { AppID = g, RoleID = roleId }));
            ret = context.SaveChanges() > 0;
        }
        catch (Exception)
        {
            throw;
        }
        return ret;
    }
}
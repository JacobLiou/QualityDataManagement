﻿using Bootstrap.Security.Blazor;
using QualityData.DbAccess.Models;
using QualityData.Service.IServices;

namespace QualityData.Service.Services;

public class AdminService : IBootstrapAdminService
{
    private IUser User { get; set; }

    private INavigation Navigations { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="user"></param>
    /// <param name="navigations"></param>
    public AdminService(IUser user, INavigation navigations)
    {
        User = user;
        Navigations = navigations;
    }

    /// <summary>
    /// 通过用户名获取角色集合方法
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public List<string> GetRoles(string userName) => User.GetRoles(userName);

    /// <summary>
    /// 通过用户名获取授权 App 集合方法
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public List<string> GetApps(string userName) => User.GetApps(userName);

    /// <summary>
    /// 通过用户名检查当前请求 Url 是否已授权方法
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public Task<bool> AuthorizingNavigation(string userName, string url)
    {
        var ret = Navigations.GetAllMenus(userName).Any(m => m.Url.Contains(url, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(ret);
    }

    /// <summary>
    /// 通过用户名检查当前请求 Url 是否已授权方法
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="url"></param>
    /// <param name="blockName"></param>
    /// <returns></returns>
    public bool AuthorizingBlock(string userName, string url, string blockName)
    {
        var ret = User.GetRoles(userName).Any(i => i.Equals("Administrators", StringComparison.OrdinalIgnoreCase));
        if (!ret)
        {
            var menus = Navigations.GetAllMenus(userName);
            var menu = menus.FirstOrDefault(m => m.Url.Contains(url, StringComparison.OrdinalIgnoreCase));
            if (menu != null)
            {
                ret = menus.Any(m => m.ParentId == menu.Id && m.IsResource == EnumResource.Block && m.Url.Equals(blockName, StringComparison.OrdinalIgnoreCase));
            }
        }
        return ret;
    }
}
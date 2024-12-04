using QualityData.DbAccess.Models;

namespace QualityData.Service.IServices;

public interface INavigation
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    List<Navigation> GetAllMenus(string userName);

    /// <summary>
    ///
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    List<string> GetMenusByRoleId(string? roleId);

    /// <summary>
    ///
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="menuIds"></param>
    /// <returns></returns>
    bool SaveMenusByRoleId(string? roleId, List<string> menuIds);
}
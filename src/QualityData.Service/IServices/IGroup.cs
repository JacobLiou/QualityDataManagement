﻿using QualityData.DbAccess.Models;

namespace QualityData.Service.IServices;

public interface IGroup
{
    /// <summary>
    /// 获得所有用户
    /// </summary>
    /// <returns></returns>
    List<Group> GetAll();

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    List<string> GetGroupsByUserId(string? userId);

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="groupIds"></param>
    /// <returns></returns>
    bool SaveGroupsByUserId(string? userId, IEnumerable<string> groupIds);

    /// <summary>
    ///
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    List<string> GetGroupsByRoleId(string? roleId);

    /// <summary>
    ///
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="groupIds"></param>
    /// <returns></returns>
    bool SaveGroupsByRoleId(string? roleId, IEnumerable<string> groupIds);
}
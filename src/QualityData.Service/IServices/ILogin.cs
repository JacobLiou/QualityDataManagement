﻿namespace QualityData.Service.IServices;

/// <summary>
/// 登录服务
/// </summary>
public interface ILogin
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="IP"></param>
    /// <param name="OS"></param>
    /// <param name="browser"></param>
    /// <param name="address"></param>
    /// <param name="userAgent"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    bool Log(string userName, string? IP, string? OS, string? browser, string? address, string? userAgent, bool result);
}
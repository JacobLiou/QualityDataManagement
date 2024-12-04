using Microsoft.EntityFrameworkCore;
using QualityData.DbAccess;
using QualityData.DbAccess.Models;
using QualityData.Service.IServices;

namespace QualityData.Service.Services;

internal class LoginService(IDbContextFactory<QualityDataContext> dbFactory) : ILogin
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
    /// <exception cref="NotImplementedException"></exception>
    public bool Log(string userName, string? IP, string? OS, string? browser, string? address, string? userAgent, bool result)
    {
        using var context = dbFactory.CreateDbContext();

        var loginUser = new LoginLog()
        {
            UserName = userName,
            LoginTime = DateTime.Now,
            Ip = IP,
            City = address,
            OS = OS,
            Browser = browser,
            UserAgent = userAgent,
            Result = result ? "登录成功" : "登录失败"
        };
        context.Add(loginUser);
        return context.SaveChanges() > 0;
    }
}
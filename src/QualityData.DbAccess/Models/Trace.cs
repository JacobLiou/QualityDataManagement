﻿using System.ComponentModel.DataAnnotations;

namespace QualityData.DbAccess.Models;


public class Trace
{
    /// <summary>
    ///
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "登录用户")]
    public string? UserName { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "操作时间")]
    public DateTime LogTime { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "登录主机")]
    public string? Ip { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "浏览器")]
    public string? Browser { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "操作系统")]
    public string? OS { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "操作地点")]
    public string? City { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "操作页面")]
    public string? RequestUrl { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Referer { get; set; }
}
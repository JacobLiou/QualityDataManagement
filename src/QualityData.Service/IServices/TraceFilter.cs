﻿namespace QualityData.Service.IServices;

public class TraceFilter
{
    /// <summary>
    ///
    /// </summary>
    public DateTime Star { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? RequestUrl { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Ip { get; set; }
}
﻿using Microsoft.EntityFrameworkCore;
using QualityData.DbAccess.Models;
using System.Diagnostics.CodeAnalysis;

namespace QualityData.DbAccess;

public class QualityDataContext(DbContextOptions<QualityDataContext> options) : DbContext(options)
{
    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<Dict>? Dicts { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<User>? Users { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<Role>? Roles { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<UserRole>? UserRole { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<Navigation>? Navigations { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<NavigationRole>? NavigationRole { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<Group>? Groups { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<UserGroup>? UserGroup { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<RoleGroup>? RoleGroup { get; set; }

    /// <summary>
    ///
    /// </summary>
    [NotNull]
    public DbSet<RoleApp>? RoleApp { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Configure();
    }
}
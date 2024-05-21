using System;
using Domain.Entities;
using Domain.Providers;
using Domain.SmsMessages;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore;

namespace Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        builder.ConfigureSmartEnum();

        base.OnModelCreating(builder);
    }

    public DbSet<SMS> SMS { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Sender> Senders { get; set; }
}
using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Infra.Data.Mappings;
using Task = TeamPlan.Domain.BackOffice.Entities.Task;

namespace TeamPlan.Infra.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<User>Users { get; set; }
    public DbSet<Task>Tasks { get; set; }
    public DbSet<Owner>Owners { get; set; }
    public DbSet<Team>Teams { get; set; }
    public DbSet<Member>Members { get; set; }
    public DbSet<Enterprise>Enterprises { get; set; }
    public DbSet<RecurringTask>RecurringTasks{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecurringTaskMap());
        modelBuilder.ApplyConfiguration(new EnterpriseMap());
        modelBuilder.ApplyConfiguration(new MemberMap());
        modelBuilder.ApplyConfiguration(new OwnerMap());
        modelBuilder.ApplyConfiguration(new TaskMap());
        modelBuilder.ApplyConfiguration(new TeamMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }
}
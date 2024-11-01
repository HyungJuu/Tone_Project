using LoginApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.DbContexts;

public partial class UserInfoContext : DbContext
{
    public UserInfoContext()
    {
    }

    public UserInfoContext(DbContextOptions<UserInfoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ToneProject;Username=postgres;Password=0308");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.ToTable("UserInfo");

            entity.Property(e => e.Id).HasMaxLength(30);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Pwd).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

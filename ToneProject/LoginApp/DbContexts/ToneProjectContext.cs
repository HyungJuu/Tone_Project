using LoginApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.DbContexts;

public partial class ToneProjectContext : DbContext
{
    public ToneProjectContext()
    {
    }

    public ToneProjectContext(DbContextOptions<ToneProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SnakeGameHistory> SnakeGameHistories { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ToneProject;Username=postgres;Password=0308");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SnakeGameHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SnakeGameHistories_pkey");

            entity.ToTable("SnakeGameHistory");

            entity.Property(e => e.PlayedDate).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.UserId).HasMaxLength(30);

            entity.HasOne(d => d.User).WithMany(p => p.SnakeGameHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_userId");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserInfo");

            entity.Property(e => e.UserId).HasMaxLength(30);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Pwd).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

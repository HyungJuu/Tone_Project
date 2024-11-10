using LoginApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.DbContexts;

public partial class SnakeGameContext : DbContext
{
    public SnakeGameContext()
    {
    }

    public SnakeGameContext(DbContextOptions<SnakeGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SnakeGameRecord> SnakeGameRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ToneProject;Username=postgres;Password=0308");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SnakeGameRecord>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.PlayedDate).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.UserId).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

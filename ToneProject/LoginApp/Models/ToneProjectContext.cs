using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.Models;

public partial class ToneProjectContext : DbContext
{
    public ToneProjectContext()
    {
    }

    public ToneProjectContext(DbContextOptions<ToneProjectContext> options)
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
            entity.HasKey(e => e.Id).HasName("userinfo_new_pkey1");

            entity.ToTable("UserInfo");

            entity.Property(e => e.Id)
                .HasMaxLength(30)
                .HasColumnName("id");
            entity.Property(e => e.Birth).HasColumnName("birth");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Order)
                .HasDefaultValueSql("nextval('userinfo_new_order_seq1'::regclass)")
                .HasColumnName("order");
            entity.Property(e => e.Pwd)
                .HasMaxLength(30)
                .HasColumnName("pwd");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

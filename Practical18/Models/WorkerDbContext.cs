using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practical18.Models;

public partial class WorkerDbContext : DbContext
{
    public WorkerDbContext()
    {
    }

    public WorkerDbContext(DbContextOptions<WorkerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<WorkersInfo> WorkersInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=WorkerDB;User=исп-34;Password=1234567890;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<WorkersInfo>(entity =>
        {
            
            entity.HasKey(e => e.WorkerId).HasName("PK__WorkersI__077C8806924FB498");

            entity.ToTable("WorkersInfo");

            entity.Property(e => e.WorkerId)
            .ValueGeneratedOnAdd()
            .HasColumnName("WorkerID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(100);
            entity.Property(e => e.SalaryAmount).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab11_12.Models;

public partial class Lab12Context : DbContext
{
    public Lab12Context()
    {
    }

    public Lab12Context(DbContextOptions<Lab12Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TMergejil> TMergejils { get; set; }

    public virtual DbSet<TTenhim> TTenhims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=lab12;User Id=sa;Password=admin123; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TMergejil>(entity =>
        {
            entity.HasKey(e => e.Mid);

            entity.ToTable("t_mergejil");

            entity.Property(e => e.Mid)
                .HasMaxLength(50)
                .HasColumnName("mid");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Tid).HasColumnName("tid");

            entity.HasOne(d => d.TidNavigation).WithMany(p => p.TMergejils)
                .HasForeignKey(d => d.Tid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_t_mergejil_t_mergejil");
        });

        modelBuilder.Entity<TTenhim>(entity =>
        {
            entity.ToTable("t_tenhim");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

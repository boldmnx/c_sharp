using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab7.Models;

public partial class Lab7Context : DbContext
{
    public Lab7Context()
    {
    }

    public Lab7Context(DbContextOptions<Lab7Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TMovie> TMovies { get; set; }

    public virtual DbSet<TTurul> TTuruls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=lab7;User Id=sa;Password=admin123; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TMovie>(entity =>
        {
            entity.ToTable("t_movie");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Rating).HasColumnName("rating").HasColumnType("float");
            entity.Property(e => e.Released).HasColumnName("released");
            entity.Property(e => e.Tid).HasColumnName("tid");

            entity.HasOne(d => d.TidNavigation).WithMany(p => p.TMovies)
                .HasForeignKey(d => d.Tid)
                .HasConstraintName("FK_t_movie_t_movie");
        });

        modelBuilder.Entity<TTurul>(entity =>
        {
            entity.ToTable("t_turul");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

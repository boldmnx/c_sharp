using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab9_10.Models;

public partial class Lect2Context : DbContext
{
    public Lect2Context()
    {
    }

    public Lect2Context(DbContextOptions<Lect2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=lect2;User Id=sa;Password=admin123; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Bid);

            entity.ToTable("branch");

            entity.Property(e => e.Bid).HasColumnName("bid");
            entity.Property(e => e.Bname)
                .HasMaxLength(255)
                .UseCollation("Latin1_General_100_CI_AS_SC_UTF8")
                .HasColumnName("bname");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Wid);

            entity.ToTable("worker");

            entity.Property(e => e.Wid).HasColumnName("wid");
            entity.Property(e => e.Bid).HasColumnName("bid");
            entity.Property(e => e.Wname)
                .HasMaxLength(50)
                .HasColumnName("wname");

            entity.HasOne(d => d.BidNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.Bid)
                .HasConstraintName("FK_worker_branch");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

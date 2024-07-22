using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PhenikaaX.Entities;

public partial class PhenikaaXContext : DbContext
{
    public PhenikaaXContext()
    {
    }

    public PhenikaaXContext(DbContextOptions<PhenikaaXContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Diagnose> Diagnoses { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=PhenikaaXDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diagnose>(entity =>
        {
            entity.HasKey(e => e.DiagnoseId).HasName("PK__diagnose__A5DCA5327B26ECB3");

            entity.Property(e => e.DiagnoseId).ValueGeneratedNever();

            entity.HasOne(d => d.Patient).WithMany(p => p.Diagnoses).HasConstraintName("FK__diagnose__patien__398D8EEE");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__patient__4D5CE476D0D0298D");

            entity.Property(e => e.PatientId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

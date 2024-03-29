﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Models;

public partial class HospitalManagementContext : DbContext
{
    public HospitalManagementContext()
    {
    }

    public HospitalManagementContext(DbContextOptions<HospitalManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<TreatmentRecord> TreatmentRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=COGNINE-L152\\SQLEXPRESS;Initial Catalog=HospitalManagement;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedicalHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId);

            entity.ToTable("MedicalHistory");

            entity.Property(e => e.HistoryId).ValueGeneratedNever();
            entity.Property(e => e.MedicalCondition).IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Treatment)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalHistories)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalHistory_PatientId");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.PatientId)
                .ValueGeneratedNever()
                .HasColumnName("PatientID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<TreatmentRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId);

            entity.Property(e => e.RecordId).ValueGeneratedNever();
            entity.Property(e => e.Outcome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.TreatmentType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.TreatmentRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TreatmentRecords_PatientId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

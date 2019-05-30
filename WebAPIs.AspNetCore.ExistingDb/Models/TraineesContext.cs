﻿using Microsoft.EntityFrameworkCore;

namespace WebAPIs.AspNetCore.ExistingDb.Models
{
    public partial class TraineesContext : DbContext
    {
        public TraineesContext()
        {
        }

        public TraineesContext(DbContextOptions<TraineesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestSubject> TestSubject { get; set; }
        public virtual DbSet<Trainee> Trainee { get; set; }
        public virtual DbSet<TraineeTest> TraineeTest { get; set; }

            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubjectCode);

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TestSubject>(entity =>
            {
                entity.Property(e => e.SubjectCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.SubjectNav)
                    .WithMany(p => p.TestSubjectNav)
                    .HasForeignKey(d => d.SubjectCode)
                    .HasConstraintName("TestSubject_Subject_FK");

                entity.HasOne(d => d.TestNav)
                    .WithMany(p => p.TestSubjectNav)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("TestSubject_Test_FK");
            });

            modelBuilder.Entity<Trainee>(entity =>
            {
                entity.Property(e => e.TraineeName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TraineeTest>(entity =>
            {
                entity.Property(e => e.TestStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.TestNav)
                    .WithMany(p => p.TraineeTestNav)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("TraineeTest_Test_FK");

                entity.HasOne(d => d.TraineeNav)
                    .WithMany(p => p.TraineeTestNav)
                    .HasForeignKey(d => d.TraineeId)
                    .HasConstraintName("TraineeTest_Trainee_FK");
            });
        }
    }
}

// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WorkSchedule.Entities;
/*using WorkSchedule.Entities;
*/
namespace WorkSchedule.DAL
{
    internal partial class WorkScheduleContext : DbContext
    {
        public WorkScheduleContext()
        {
        }

        public WorkScheduleContext(DbContextOptions<WorkScheduleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<PlacementContract> PlacementContracts { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        //public virtual DbSet<DbVersion> DbVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.HomePhone).IsFixedLength();
            });

            modelBuilder.Entity<EmployeeSkill>(entity =>
            {
                entity.Property(e => e.Level).HasComment("Levels may be 1=Novice, 2=Proficient, 3=Expert");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSkill)
                    .HasForeignKey(d => d.EmployeeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeSkills_Employees");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.EmployeeSkills)
                    .HasForeignKey(d => d.SkillID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeSkills_Skills");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Phone).IsFixedLength();

                entity.Property(e => e.Province).IsFixedLength();
            });

            modelBuilder.Entity<PlacementContract>(entity =>
            {
                entity.Property(e => e.Title).HasDefaultValueSql("('TBA')");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PlacementContracts)
                    .HasForeignKey(d => d.LocationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlacementContracts_Locations");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.EmployeeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedules_Employees");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ShiftID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedules_Shifts");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.Property(e => e.Notes).HasComment("Information about required skills, entry location, keycards, etc.");

                entity.HasOne(d => d.PlacementContract)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.PlacementContractID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shifts_Locations");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.RequiresTicket).HasComment("True if some kind of Journeyman or other certification is required for this skill");

                entity.HasMany(d => d.Contracts)
                    .WithMany(p => p.Skills)
                    .UsingEntity<Dictionary<string, object>>(
                        "ContractSkill",
                        l => l.HasOne<PlacementContract>().WithMany().HasForeignKey("ContractID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ContractSkills_PlacementContracts"),
                        r => r.HasOne<Skill>().WithMany().HasForeignKey("SkillID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ContractSkills_Skills"),
                        j =>
                        {
                            j.HasKey("SkillID", "ContractID").HasName("PK__Contract__533042A74133E1F2");

                            j.ToTable("ContractSkills");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
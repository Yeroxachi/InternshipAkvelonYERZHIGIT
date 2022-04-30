using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace InternshipAkvelonYERZHIGIT.Models
{
    public partial class ProjectsContext : DbContext
    {
        public ProjectsContext()
        {
        }

        public ProjectsContext(DbContextOptions<ProjectsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost; Port=5432; User Id=postgres; Password=12345; Database=Projects;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Kazakhstan.1251");

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CompletionDate)
                    .HasColumnType("date")
                    .HasColumnName("completion_date");

                entity.Property(e => e.Descroiption)
                    .IsRequired()
                    .HasColumnName("descroiption");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.TaskId)
                    .HasColumnName("task_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Task_Project_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

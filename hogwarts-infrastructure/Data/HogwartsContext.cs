using System;
using System.Threading.Tasks;
using hogwarts_core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace hogwarts_infrastructure.Data
{
    public partial class HogwartsContext : DbContext
    {
        public HogwartsContext()
        {
        }

        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        { 
        }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("applications");

                entity.HasIndex(e => new { e.PersonId, e.AspiredHouse }, "applications_person_id_aspired_house_key")
                    .IsUnique();

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.AspiredHouse)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("aspired_house");

                entity.Property(e => e.PersonId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("person_id");

                entity.HasOne(d => d.AspiredHouseNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.AspiredHouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("applications_aspired_house_fkey");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("applications_person_id_fkey");
            });

            modelBuilder.Entity<House>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("houses_pkey");

                entity.ToTable("houses");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(10)
                    .HasColumnName("person_id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("last_name");
            });
        }
    }
}

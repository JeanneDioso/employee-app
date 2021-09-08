using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EmployeeApp.Models
{
    public partial class EmployeeRecordsContext : DbContext
    {
        public EmployeeRecordsContext()
        {
        }

        public EmployeeRecordsContext(DbContextOptions<EmployeeRecordsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PHLPPLT-JDIOSO;Database=EmployeeRecords;Trusted_Connection=True;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employee>(entity =>
            {
              // entity.HasNoKey();

                entity.HasKey(e => e.Id)
                .HasName("PrimaryKey_EmployeeId");

                entity.ToTable("Employee");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

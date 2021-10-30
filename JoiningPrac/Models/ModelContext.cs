using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JoiningPrac.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dept> Depts { get; set; }
        public virtual DbSet<Student1> Student1s { get; set; }
        public virtual DbSet<Student2> Student2s { get; set; }
        public virtual DbSet<Employees> Employeess { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));Persist Security Info=True;User Id=student1;Password=oracle;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("STUDENT1")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasKey(e => e.Id);

               // entity.HasNoKey();

                entity.ToTable("DEPT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Student1>(entity =>
            {
                entity.HasKey(e =>e.Id);

                entity.ToTable("STUDENT1");

                entity.Property(e => e.Age)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("AGE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Student2>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("STUDENT2");

                entity.Property(e => e.Deptid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DEPTID");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.Eid);

                entity.ToTable("EMPLOYEES");

                entity.Property(e => e.Ename)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ENAME");

                entity.Property(e => e.Eid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("EID");

                entity.Property(e => e.Did)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DID");

                entity.Property(e => e.Dgid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DGID");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Did);

                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.Dname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DNAME");

                entity.Property(e => e.Did)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DID");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(e => e.Dgid);

                entity.ToTable("DESIGNATION");

                entity.Property(e => e.Dgname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DGNAME");

                entity.Property(e => e.Dgid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DGID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.IO;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class transfersystemContext : DbContext
    {
        private readonly HttpContext _httpContext;
        private string ConnectionString { get; set; }
        IConfiguration _config;
        public transfersystemContext(DbContextOptions<transfersystemContext> options, IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            _httpContext = httpContextAccessor?.HttpContext;
        }

        public transfersystemContext(string conn)
        {
            ConnectionString = conn;
            _httpContext = null;
        }

        public virtual DbSet<UserDB> Users { get; set; }
        public virtual DbSet<CompanyDB> Companies { get; set; }
        public virtual DbSet<DepartmentDB> Departments { get; set; }
        public virtual DbSet<EmployeeDB> Employees { get; set; }
        public virtual DbSet<ObjectiveDB> Objectives { get; set; }
        public virtual DbSet<ResponsibilityDB> Responsibilities { get; set; }
        public virtual DbSet<EmployeeViewDB> EmployeeViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (_httpContext != null)
                {
                    var clientClaim = _httpContext?.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                    if (clientClaim == null) clientClaim = "Notauth";
                    optionsBuilder.UseNpgsql(Connection.GetConnection(clientClaim));
                }
                else
                {
                    optionsBuilder.UseNpgsql(ConnectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<UserDB>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("login");

                entity.ToTable("user_");

                entity.Property(e => e.Login)
                    .ValueGeneratedNever()
                    .HasColumnName("login");

                entity.Property(e => e.Password_).HasColumnName("password_");

                entity.Property(e => e.Name_).HasColumnName("name_");

                entity.Property(e => e.Surname).HasColumnName("surname");
            });

            modelBuilder.Entity<CompanyDB>(entity =>
            {
                entity.HasKey(e => e.Companyid)
                    .HasName("companyid");

                entity.ToTable("company");

                entity.Property(e => e.Companyid)
                    .ValueGeneratedNever()
                    .HasColumnName("companyid");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Foundationyear).HasColumnName("foundationyear");
            });

            modelBuilder.Entity<DepartmentDB>(entity =>
            {
                entity.HasKey(e => e.Departmentid)
                    .HasName("departmentid");

                entity.ToTable("department");

                entity.Property(e => e.Departmentid)
                    .ValueGeneratedNever()
                    .HasColumnName("departmentid");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.CompanyID).HasColumnName("company");

                entity.Property(e => e.Foundationyear).HasColumnName("foundationyear");

                entity.Property(e => e.Activityfield).HasColumnName("activityfield");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("department_company_fkey");
            });

            modelBuilder.Entity<EmployeeDB>(entity =>
            {
                entity.HasKey(e => e.Employeeid)
                    .HasName("employeeid");

                entity.ToTable("employee");

                entity.Property(e => e.Employeeid)
                    .ValueGeneratedNever()
                    .HasColumnName("employeeid");

                entity.Property(e => e.UserID).HasColumnName("user_");

                entity.Property(e => e.CompanyID).HasColumnName("company");

                entity.Property(e => e.DepartmentID).HasColumnName("department");

                entity.Property(e => e.Permission_).HasColumnName("permission_");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("employee_user_fkey");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("employee_company_fkey");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("employee_department_fkey");
            });

            modelBuilder.Entity<ObjectiveDB>(entity =>
            {
                entity.HasKey(e => e.Objectiveid)
                    .HasName("objectiveid");

                entity.ToTable("objective");

                entity.Property(e => e.Objectiveid)
                    .ValueGeneratedNever()
                    .HasColumnName("objectiveid");

                entity.Property(e => e.ParentTaskID).HasColumnName("parentobjective");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.CompanyID).HasColumnName("company");

                entity.Property(e => e.DepartmentID).HasColumnName("department");

                entity.Property(e => e.Termbegin).HasColumnName("termbegin");

                entity.Property(e => e.Termend).HasColumnName("termend");

                entity.Property(e => e.Estimatedtime).HasColumnName("estimatedtime");

                entity.HasOne(d => d.ParentTask)
                    .WithMany(p => p.SubTasks)
                    .HasForeignKey(d => d.ParentTaskID)
                    .HasConstraintName("objective_parentobjective_fkey");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Objectives)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("objective_company_fkey");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Objectives)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("objective_department_fkey");
            });

            modelBuilder.Entity<ResponsibilityDB>(entity =>
            {
                entity.HasKey(e => e.Responsibilityid)
                    .HasName("responsibilityid");

                entity.ToTable("responsibility");

                entity.Property(e => e.Responsibilityid)
                    .ValueGeneratedNever()
                    .HasColumnName("responsibilityid");

                entity.Property(e => e.EmployeeID)
                    .IsRequired()
                    .HasColumnName("employee");

                entity.Property(e => e.ObjectiveID)
                    .IsRequired()
                    .HasColumnName("objective");

                entity.Property(e => e.Timespent).HasColumnName("timespent");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Responsibilites)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("responsibility_employee_fkey");

                entity.HasOne(d => d.Objective)
                    .WithMany(p => p.Responsibilites)
                    .HasForeignKey(d => d.ObjectiveID)
                    .HasConstraintName("responsibility_objective_fkey");
            });

            modelBuilder.Entity<EmployeeViewDB>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("employeeview");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Login).HasColumnName("login");

                entity.Property(e => e.Name_).HasColumnName("name_");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.Property(e => e.Department).HasColumnName("department");

                entity.Property(e => e.Permission_).HasColumnName("permission_");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

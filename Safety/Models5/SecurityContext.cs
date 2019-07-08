using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Safety.Models
{
    public partial class SecurityContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountApplicationRole> AccountApplicationRole { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<CentroCoste> CentroCoste { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberArea> MemberArea { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        // Unable to generate entity type for table 'Authentication.Duplicates#'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=Security;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account", "Authentication");

                entity.HasIndex(e => e.Idmember)
                    .HasName("UQ_iduser")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ_UserName")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Idmember).HasColumnName("idmember");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdmemberNavigation)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.Idmember)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Accounts__iduser__46E78A0C");
            });

            modelBuilder.Entity<AccountApplicationRole>(entity =>
            {
                entity.HasKey(e => new { e.Idacc, e.Idapp });

                entity.ToTable("AccountApplicationRole", "Authentication");

                entity.Property(e => e.Idacc).HasColumnName("idacc");

                entity.Property(e => e.Idapp).HasColumnName("idapp");

                entity.Property(e => e.Idrole).HasColumnName("idrole");

                entity.HasOne(d => d.IdaccNavigation)
                    .WithMany(p => p.AccountApplicationRole)
                    .HasForeignKey(d => d.Idacc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountsA__idacc__6C190EBB");

                entity.HasOne(d => d.IdappNavigation)
                    .WithMany(p => p.AccountApplicationRole)
                    .HasForeignKey(d => d.Idapp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountsA__idapp__6D0D32F4");

                entity.HasOne(d => d.IdroleNavigation)
                    .WithMany(p => p.AccountApplicationRole)
                    .HasForeignKey(d => d.Idrole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountsA__idrol__6E01572D");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Iddependency).HasColumnName("iddependency");

                entity.HasOne(d => d.IddependencyNavigation)
                    .WithMany(p => p.Area)
                    .HasForeignKey(d => d.Iddependency)
                    .HasConstraintName("FK__Area__iddependen__3F115E1A");
            });

            modelBuilder.Entity<CentroCoste>(entity =>
            {
                entity.ToTable("CentroCoste", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datbi)
                    .HasColumnName("DATBI")
                    .HasMaxLength(255);

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.Kokrs).HasColumnName("KOKRS");

                entity.Property(e => e.Mandt).HasColumnName("MANDT");

                entity.Property(e => e.Mctxt)
                    .HasColumnName("MCTXT")
                    .HasMaxLength(255);

                entity.Property(e => e.ShortDescription).HasMaxLength(255);

                entity.Property(e => e.Spras)
                    .HasColumnName("SPRAS")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday).HasMaxLength(12);

                entity.Property(e => e.DateEntry).HasMaxLength(12);

                entity.Property(e => e.DateEntry2).HasColumnType("date");

                entity.Property(e => e.Department).HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.Funcion).HasMaxLength(100);

                entity.Property(e => e.FunctionId).HasColumnName("FunctionID");

                entity.Property(e => e.Generate).HasColumnName("generate");

                entity.Property(e => e.HorarioId)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.IEmployeeNum).HasColumnName("iEmployeeNum");

                entity.Property(e => e.IdCentral)
                    .HasColumnName("ID_Central")
                    .HasColumnType("numeric(12, 0)");

                entity.Property(e => e.Identification).HasMaxLength(15);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Mail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mail2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modificacion).HasColumnType("datetime");

                entity.Property(e => e.Movil)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.OldDescriptionPosition).HasMaxLength(100);

                entity.Property(e => e.OldFuncion).HasMaxLength(100);

                entity.Property(e => e.OldPositionId)
                    .HasColumnName("OldPositionID")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Position).HasMaxLength(100);

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.PositionIdold)
                    .HasColumnName("PositionIDOld")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.SapNv).HasColumnName("SAP_NV");

                entity.Property(e => e.Segundoapellido).HasMaxLength(30);

                entity.Property(e => e.Sexo).HasColumnType("char(1)");

                entity.Property(e => e.StatusSap).HasColumnName("StatusSAP");

                entity.Property(e => e.SubdivisiónDePersonal)
                    .HasColumnName("Subdivisión de personal")
                    .HasMaxLength(100);

                entity.Property(e => e.TextoCentroCoste)
                    .HasColumnName("Texto centro coste")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member", "Authentication");

                entity.HasIndex(e => e.EmployNumber)
                    .HasName("UQ_EmployNumber")
                    .IsUnique();

                entity.HasIndex(e => e.Ipphone)
                    .HasName("UQ_ipphone")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.EmployNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Ipphone)
                    .HasColumnName("ipphone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SurName)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MemberArea>(entity =>
            {
                entity.ToTable("MemberArea", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idarea).HasColumnName("idarea");

                entity.Property(e => e.Idmember).HasColumnName("idmember");

                entity.HasOne(d => d.IdareaNavigation)
                    .WithMany(p => p.MemberArea)
                    .HasForeignKey(d => d.Idarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberAre__idare__76619304");

                entity.HasOne(d => d.IdmemberNavigation)
                    .WithMany(p => p.MemberArea)
                    .HasForeignKey(d => d.Idmember)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberAre__idmem__756D6ECB");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdApp).HasColumnName("idApp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAppNavigation)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.IdApp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Roles__idApp__693CA210");
            });
        }
    }
}

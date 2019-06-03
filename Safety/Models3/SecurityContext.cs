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
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        // Unable to generate entity type for table 'Authentication.Duplicates#'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Startup.ConnectionString);

                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=Security;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account", "Authentication");

                entity.HasIndex(e => e.Iduser)
                    .HasName("UQ_iduser")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ_UserName")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IduserNavigation)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.Iduser)
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

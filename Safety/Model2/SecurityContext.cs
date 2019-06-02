using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Safety.Models 
{
    public partial class SecurityContext : DbContext
    {
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<AccountsApplicationsRoles> AccountsApplicationsRoles { get; set; }
        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(Startup.ConnectionString);

                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=Security;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.ToTable("Accounts", "Authentication");

                entity.HasIndex(e => e.Iduser)
                    .HasName("OTOR")
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
                    .WithOne(p => p.Accounts)
                    .HasForeignKey<Accounts>(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Accounts__iduser__46E78A0C");
            });

            modelBuilder.Entity<AccountsApplicationsRoles>(entity =>
            {
                entity.HasKey(e => new { e.Idacc, e.Idapp });

                entity.ToTable("AccountsApplicationsRoles", "Authentication");

                entity.Property(e => e.Idacc).HasColumnName("idacc");

                entity.Property(e => e.Idapp).HasColumnName("idapp");

                entity.Property(e => e.Idrole).HasColumnName("idrole");

                entity.HasOne(d => d.IdaccNavigation)
                    .WithMany(p => p.AccountsApplicationsRoles)
                    .HasForeignKey(d => d.Idacc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountsA__idacc__6C190EBB");

                entity.HasOne(d => d.IdappNavigation)
                    .WithMany(p => p.AccountsApplicationsRoles)
                    .HasForeignKey(d => d.Idapp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountsA__idapp__6D0D32F4");

                entity.HasOne(d => d.IdroleNavigation)
                    .WithMany(p => p.AccountsApplicationsRoles)
                    .HasForeignKey(d => d.Idrole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountsA__idrol__6E01572D");
            });

            modelBuilder.Entity<Applications>(entity =>
            {
                entity.ToTable("Applications", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Areas>(entity =>
            {
                entity.ToTable("Areas", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Iddependency).HasColumnName("iddependency");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("Roles", "Authentication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdApp).HasColumnName("idApp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAppNavigation)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.IdApp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Roles__idApp__693CA210");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "Authentication");

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
        }
    }
}

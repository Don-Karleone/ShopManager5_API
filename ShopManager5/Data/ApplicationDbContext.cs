using Microsoft.EntityFrameworkCore;
using ShopManager5.Api.Data.Models;

namespace ShopManager5.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.PasswordHash)
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Client>(entity =>
            {

                entity.Property(e => e.BuildingNumber)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(25)
                    .IsRequired(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nip)
                    .HasMaxLength(10)
                    .HasColumnName("NIP")
                    .IsRequired(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(9);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.BuildingNumber)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                //entity.HasOne(d => d.Client)
                //    .WithMany(p => p.Invoices)
                //    .HasForeignKey(d => d.Id)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Invoices_ClientID");

                //entity.HasOne(d => d.Employee)
                //    .WithMany(p => p.Invoices)
                //    .HasForeignKey(d => d.Id)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Invoices_EmployeeID");
            });
        }
    }
}
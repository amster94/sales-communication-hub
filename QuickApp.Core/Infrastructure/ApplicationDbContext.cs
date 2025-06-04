// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2024 www.ebenmonney.com/mit-license
// ---------------------------------------

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickApp.Core.Models;
using QuickApp.Core.Models.Account;
using QuickApp.Core.Models.Admin;
using QuickApp.Core.Models.Manager;
using QuickApp.Core.Models.SalesLead;
using QuickApp.Core.Models.SalesPerson;
using QuickApp.Core.Models.Shop;
using QuickApp.Core.Models.Team;
using QuickApp.Core.Services.Account;
using System.Reflection.Emit;

namespace QuickApp.Core.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions options, IUserIdAccessor userIdAccessor) :
        IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<ManagerModel> Managers { get; set; }
        public DbSet<SalesTeamModel> SalesTeam { get; set; }
        public DbSet<LeadModel> Leads { get; set; }
        public DbSet<TeamModel> Teams { get; set; }
        public DbSet<TeamManagerModel> TeamManager { get; set; }
        public DbSet<TeamSalesPersonModel> TeamSalesPerson { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string priceDecimalType = "decimal(18,2)";
            const string tablePrefix = "";

            builder.Entity<AdminModel>().HasKey(b => b.admin_id)
        .HasName("PrimaryKey_admin_id");
            builder.Entity<ManagerModel>().HasKey(b => b.manager_id)
        .HasName("PrimaryKey_manager_id");
            builder.Entity<SalesTeamModel>().HasKey(b => b.sales_rep_id)
        .HasName("PrimaryKey_sales_rep_id");
            builder.Entity<LeadModel>().HasKey(b => b.lead_id)
        .HasName("PrimaryKey_lead_id");
            builder.Entity<TeamModel>().HasKey(b => b.team_id)
        .HasName("PrimaryKey_team_id");



            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Claims)
                .WithOne()
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Roles)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>()
                .HasMany(r => r.Claims)
                .WithOne()
                .HasForeignKey(c => c.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>()
                .HasMany(r => r.Users)
                .WithOne()
                .HasForeignKey(r => r.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TeamManagerModel>()
            .HasKey(tm => new { tm.team_id, tm.manager_id }); // Composite PK

            builder.Entity<TeamManagerModel>()
                .HasOne(tm => tm.Team)
                .WithMany(tm => tm.TeamManagers)
                .HasForeignKey(tm => tm.team_id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TeamManagerModel>()
                .HasOne(tm => tm.Manager)
                .WithMany(m => m.TeamManagers)
                .HasForeignKey(tm => tm.manager_id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TeamSalesPersonModel>()
            .HasKey(ts => new { ts.team_id, ts.sales_rep_id });

            builder.Entity<TeamSalesPersonModel>()
                .HasOne(ts => ts.Team)
                .WithMany(t => t.TeamSalesPerson)
                .HasForeignKey(ts => ts.team_id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TeamSalesPersonModel>()
                .HasOne(ts => ts.SalesTeam)
                .WithMany(s => s.TeamSalesPerson)
                .HasForeignKey(ts => ts.sales_rep_id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.Name);
            builder.Entity<Customer>().Property(c => c.Email).HasMaxLength(100);
            builder.Entity<Customer>().Property(c => c.PhoneNumber).IsUnicode(false).HasMaxLength(30);
            builder.Entity<Customer>().Property(c => c.City).HasMaxLength(50);
            builder.Entity<Customer>().ToTable($"{tablePrefix}{nameof(Customers)}");

            builder.Entity<ProductCategory>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<ProductCategory>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<ProductCategory>().ToTable($"{tablePrefix}{nameof(ProductCategories)}");

            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Product>().HasIndex(p => p.Name);
            builder.Entity<Product>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<Product>().Property(p => p.Icon).IsUnicode(false).HasMaxLength(256);
            builder.Entity<Product>().HasOne(p => p.Parent).WithMany(p => p.Children).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>().Property(p => p.BuyingPrice).HasColumnType(priceDecimalType);
            builder.Entity<Product>().Property(p => p.SellingPrice).HasColumnType(priceDecimalType);
            builder.Entity<Product>().ToTable($"{tablePrefix}{nameof(Products)}");

            builder.Entity<Order>().Property(o => o.Comments).HasMaxLength(500);
            builder.Entity<Order>().Property(p => p.Discount).HasColumnType(priceDecimalType);
            builder.Entity<Order>().ToTable($"{tablePrefix}{nameof(Orders)}");

            builder.Entity<OrderDetail>().Property(p => p.UnitPrice).HasColumnType(priceDecimalType);
            builder.Entity<OrderDetail>().Property(p => p.Discount).HasColumnType(priceDecimalType);
            builder.Entity<OrderDetail>().ToTable($"{tablePrefix}{nameof(OrderDetails)}");

            builder.Entity<AdminModel>().Property(a => a.first_name).IsRequired().HasMaxLength(100);
            builder.Entity<AdminModel>().Property(a => a.last_name).IsRequired().HasMaxLength(100);
            builder.Entity<AdminModel>().Property(a => a.email).HasMaxLength(100);
            builder.Entity<AdminModel>().Property(a => a.phone_number).IsUnicode(false).HasMaxLength(30);
            builder.Entity<AdminModel>().Property(a => a.work_role).IsRequired().HasMaxLength(100);

            builder.Entity<ManagerModel>().Property(m => m.first_name).IsRequired().HasMaxLength(100);
            builder.Entity<ManagerModel>().Property(m => m.last_name).IsRequired().HasMaxLength(100);
            builder.Entity<ManagerModel>().Property(m => m.email).HasMaxLength(100);
            builder.Entity<ManagerModel>().Property(m => m.phone_number).IsUnicode(false).HasMaxLength(30);
            builder.Entity<ManagerModel>().Property(m => m.work_role).IsRequired().HasMaxLength(100);

            builder.Entity<SalesTeamModel>().Property(s => s.first_name).IsRequired().HasMaxLength(100);
            builder.Entity<SalesTeamModel>().Property(s => s.last_name).IsRequired().HasMaxLength(100);
            builder.Entity<SalesTeamModel>().Property(s => s.email).HasMaxLength(100);
            builder.Entity<SalesTeamModel>().Property(s => s.phone_number).IsUnicode(false).HasMaxLength(30);
            builder.Entity<SalesTeamModel>().Property(s => s.work_role).IsRequired().HasMaxLength(100);

            builder.Entity<LeadModel>().Property(l => l.lead_name).IsRequired().HasMaxLength(100);
            builder.Entity<LeadModel>().Property(l => l.email).HasMaxLength(100);
            builder.Entity<LeadModel>().Property(l => l.phone_number).IsUnicode(false).HasMaxLength(30);
            builder.Entity<LeadModel>().Property(l => l.product_interest).IsRequired().HasMaxLength(255);
            builder.Entity<LeadModel>().Property(l => l.lead_type).IsRequired().HasMaxLength(100);
            builder.Entity<LeadModel>().Property(l => l.lead_source).IsRequired().HasMaxLength(100);
            builder.Entity<LeadModel>().Property(l => l.requirement).HasColumnType("TEXT");
            builder.Entity<LeadModel>().Property(l => l.expected_budget).IsRequired().HasMaxLength(100);
            builder.Entity<LeadModel>().Property(l => l.submission_date).IsRequired().HasColumnType("datetime").HasDefaultValueSql("SYSDATETIME()");
            builder.Entity<LeadModel>().Property(l => l.status).IsRequired().HasMaxLength(100);

            builder.Entity<TeamModel>().Property(t => t.team_name).IsRequired().HasMaxLength(100);
            builder.Entity<TeamModel>().Property(t => t.team_description).HasColumnType("TEXT");
            builder.Entity<TeamModel>().Property(t => t.region).IsRequired().HasMaxLength(100);
            builder.Entity<TeamModel>().Property(t => t.creation_date).IsRequired().HasColumnType("datetime").HasDefaultValueSql("SYSDATETIME()");
            builder.Entity<TeamModel>().Property(t => t.active_status).IsRequired().HasColumnType("bit");
            builder.Entity<TeamModel>().Property(t => t.performance_rating).IsRequired().HasMaxLength(50);
            builder.Entity<TeamModel>().Property(t => t.last_updated).IsRequired().HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
            builder.Entity<TeamModel>().Property(t => t.is_virtual_team).IsRequired().HasColumnType("bit");

        }

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddAuditInfo()
        {
            var currentUserId = userIdAccessor.GetCurrentUserId();

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity &&
                           (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = currentUserId;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = currentUserId;
            }
        }
    }
}

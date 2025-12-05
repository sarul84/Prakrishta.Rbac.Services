using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;

namespace RbacService.Infrastructure.Data
{
    public class RbacDbContext : DbContext
    {
        public RbacDbContext(DbContextOptions<RbacDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<OrgAccessMapping> OrgAccessMappings => Set<OrgAccessMapping>();
        public DbSet<Enumeration> Enumerations => Set<Enumeration>();
        public DbSet<PiiField> PiiFields => Set<PiiField>();
        public DbSet<MaskingRule> MaskingRules => Set<MaskingRule>();
        public DbSet<RoleMaskingRule> RoleMaskingRules => Set<RoleMaskingRule>();
        public DbSet<PiiAccessLog> PiiAccessLogs => Set<PiiAccessLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User ↔ UserRole ↔ Role (many-to-many)
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Role ↔ RolePermission ↔ Permission (many-to-many)
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // User ↔ Organization (one-to-many)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Organization)
                .WithMany(o => o.Users)
                .HasForeignKey(u => u.OrganizationId);

            // Organization ↔ Department (one-to-many)
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Organization)
                .WithMany(o => o.Departments)
                .HasForeignKey(d => d.OrganizationId);

            // Organization ↔ OrgAccessMapping (self-referencing many-to-many)
            modelBuilder.Entity<OrgAccessMapping>()
                .HasKey(oam => new { oam.SourceOrganizationId, oam.TargetOrganizationId });

            // PiiField ↔ MaskingRule (one-to-many)
            modelBuilder.Entity<MaskingRule>()
                .HasOne(mr => mr.PiiField)
                .WithMany(pf => pf.MaskingRules)
                .HasForeignKey(mr => mr.PiiFieldId);

            // Role ↔ RoleMaskingRule ↔ MaskingRule (many-to-many)
            modelBuilder.Entity<RoleMaskingRule>()
                .HasKey(rmr => new { rmr.RoleId, rmr.MaskingRuleId });

            modelBuilder.Entity<PiiAccessLog>()
                .HasKey(pal => pal.AccessLogId);

            modelBuilder.Entity<RoleMaskingRule>()
                .HasOne(rmr => rmr.MaskingRule)
                .WithMany(mr => mr.RoleMaskingRules)
                .HasForeignKey(rmr => rmr.MaskingRuleId);

            // Soft delete filters (for all auditable entities)
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Permission>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Organization>().HasQueryFilter(o => !o.IsDeleted);
            modelBuilder.Entity<Department>().HasQueryFilter(d => !d.IsDeleted);
            modelBuilder.Entity<PiiField>().HasQueryFilter(pf => !pf.IsDeleted);
            modelBuilder.Entity<MaskingRule>().HasQueryFilter(mr => !mr.IsDeleted);
            modelBuilder.Entity<Enumeration>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<PiiAccessLog>().HasQueryFilter(pal => !pal.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(ur => !ur.IsDeleted);
            modelBuilder.Entity<RolePermission>().HasQueryFilter(rp => !rp.IsDeleted);
            modelBuilder.Entity<RoleMaskingRule>().HasQueryFilter(rmr => !rmr.IsDeleted);
            modelBuilder.Entity<OrgAccessMapping>().HasQueryFilter(oam => !oam.IsDeleted);
            modelBuilder.Entity<RbacService.Domain.Entities.Application>().HasQueryFilter(a =>  !a.IsDeleted);
        }
    }
}

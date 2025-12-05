using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RbacService.Domain.Interfaces;

namespace RbacService.Infrastructure.Interceptors
{
    public class AuditInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private string GetCurrentUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value
                   ?? _httpContextAccessor.HttpContext?.User?.Identity?.Name
                   ?? "system";
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            ApplyAudit(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            ApplyAudit(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ApplyAudit(DbContext? context)
        {
            if (context == null) return;

            var userEmail = GetCurrentUserEmail();

            var entries = context.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditableEntity &&
                            (e.State == EntityState.Added ||
                             e.State == EntityState.Modified ||
                             e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var auditable = (IAuditableEntity)entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditable.CreatedAt = DateTime.UtcNow;
                        auditable.CreatedBy = userEmail;
                        break;
                    case EntityState.Modified:
                        auditable.UpdatedAt = DateTime.UtcNow;
                        auditable.UpdatedBy = userEmail;
                        break;
                    case EntityState.Deleted:
                        auditable.DeletedAt = DateTime.UtcNow;
                        auditable.DeletedBy = userEmail;
                        auditable.IsDeleted = true;
                        entry.State = EntityState.Modified; // soft delete
                        break;
                }
            }
        }
    }
}

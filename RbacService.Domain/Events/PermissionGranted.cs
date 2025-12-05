
namespace RbacService.Domain.Events
{
    public record PermissionGranted(Guid roleId, Guid permissionId);
}

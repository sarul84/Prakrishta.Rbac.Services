namespace RbacService.Application.Roles.Commands
{
    public record CreateRole
    (
        string Name,
        string Description,
        Guid OrganizationId,
        Guid ApplicationId,
        string? scopeType,
        IEnumerable<Guid> PermissionIds,
        IEnumerable<Guid> UserIds
    );
}

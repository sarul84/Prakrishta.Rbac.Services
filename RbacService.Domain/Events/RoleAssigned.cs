namespace RbacService.Domain.Events
{
    public record RoleAssigned(Guid userId, Guid roleId);
}

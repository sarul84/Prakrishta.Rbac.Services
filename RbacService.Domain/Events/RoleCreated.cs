namespace RbacService.Domain.Events
{
    public record RoleCreated(Guid roleId, string name);
}

namespace RbacService.Domain.Events
{
    public record UserAdded(Guid userId, string email, Guid organizationId, Guid applicationId);
}

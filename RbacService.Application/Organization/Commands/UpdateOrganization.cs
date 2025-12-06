namespace RbacService.Application.Organization.Commands
{
    public record UpdateOrganization
    (
        Guid OrganizationId,
        string Name,
        string? Description,
        string? Type,
        Guid? ParentOrganizationId
    );
}

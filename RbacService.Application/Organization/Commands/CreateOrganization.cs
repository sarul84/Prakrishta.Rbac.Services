namespace RbacService.Application.Organization.Commands
{
    public record CreateOrganization
    (
        string Name,
        string? Description,
        string? Type,
        Guid? ParentOrganizationId
    );
}

namespace RbacService.Application.DTOs.Organization
{
    public record OrganizationUpdateDto(
        Guid OrganizationId,
        string Name,
        string? Description,
        string? Type,
        Guid? ParentOrganizationId
    );
}

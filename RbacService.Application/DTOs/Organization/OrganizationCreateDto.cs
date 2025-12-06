namespace RbacService.Application.DTOs.Organization
{
    public record OrganizationCreateDto(
        string Name,
        string? Description,
        string? Type,
        Guid? ParentOrganizationId
    );
}

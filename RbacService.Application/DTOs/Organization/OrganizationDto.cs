namespace RbacService.Application.DTOs.Organization
{
    public record OrganizationDto(
        Guid Id,
        string Name,
        string? Description,
        string? Type,
        Guid? ParentOrganizationId
    );
}

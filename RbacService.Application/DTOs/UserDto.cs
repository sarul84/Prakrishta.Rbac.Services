namespace RbacService.Application.DTOs
{
    public record UserDto (
        Guid UserId,
        string Name,
        string Email,
        string? Designation,
        Guid OrganizationId,
        Guid ApplicationId,
        Guid? DepartmentId,
        Guid? ManagerId,
        DateTime? LastLoginAt);
}

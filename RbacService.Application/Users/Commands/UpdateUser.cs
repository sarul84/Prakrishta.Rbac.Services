namespace RbacService.Application.Users.Commands
{
    public record UpdateUser(
        Guid UserId,
        string Name,
        string Email,
        string? Designation,
        Guid OrganizationId,
        Guid ApplicationId,
        Guid? DepartmentId,        
        Guid? ManagerId,        
        DateTime? LastLoginAt
    );
}

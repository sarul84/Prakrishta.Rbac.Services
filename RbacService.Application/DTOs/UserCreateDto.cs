namespace RbacService.Application.DTOs
{
    public record UserCreateDto(
        string Name,
        string Email,        
        string? Designation,
        Guid OrganizationId,
        Guid ApplicationId,
        Guid? DepartmentId,        
        Guid? ManagerId
    );

}

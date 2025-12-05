namespace RbacService.Application.Users.Commands
{
    public record CreateUser(string Name, 
        string Email, 
        string? Designation, 
        Guid OrganizationId, 
        Guid ApplicationId, 
        Guid? ManagerId, 
        Guid? DepartmentId);
}

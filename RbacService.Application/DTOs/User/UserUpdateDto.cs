using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Application.DTOs.User
{
    public record UserUpdateDto(
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

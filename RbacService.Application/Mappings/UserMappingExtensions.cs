using RbacService.Application.DTOs;
using RbacService.Application.Users.Commands;
using RbacService.Domain.Entities;

namespace RbacService.Application.Mappings
{
    public static class UserMappingExtensions
    {
        public static CreateUser ToCommand(this UserCreateDto dto) =>
            new(dto.Name, dto.Email,  dto.Designation,
                dto.OrganizationId, dto.ApplicationId, dto.DepartmentId, dto.ManagerId);

        public static UpdateUser ToCommand(this UserUpdateDto dto) =>
            new(dto.UserId, dto.Name, dto.Email, dto.Designation,
                dto.OrganizationId, dto.ApplicationId, dto.DepartmentId, dto.ManagerId, dto.LastLoginAt);

        public static UserDto ToDto(this User entity) =>
            new(entity.UserId, entity.Name, entity.Email, entity.Designation, entity.OrganizationId, 
                entity.ApplicationId, entity.DepartmentId, entity.ManagerId, entity.LastLoginAt);

    }
}

using RbacService.Application.DTOs.Organization;
using RbacService.Application.Organization.Commands;

namespace RbacService.Application.Mappings
{
    public static class OrganizationMappingExtensions
    {
        public static CreateOrganization ToCommand(this OrganizationCreateDto dto) =>
            new(dto.Name,
                dto.Description,
                dto.Type,
                dto.ParentOrganizationId
            );

        public static UpdateOrganization ToCommand(this OrganizationUpdateDto dto) => 
            new(dto.OrganizationId,
                dto.Name,
                dto.Description,
                dto.Type,
                dto.ParentOrganizationId
            );

        public static OrganizationDto ToDto(this RbacService.Domain.Entities.Organization entity) =>
            new(entity.OrganizationId,
                entity.Name,
                entity.Description,
                entity.Type,
                entity.ParentOrganizationId
            );
            
    }
}

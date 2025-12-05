namespace RbacService.Application.Users.Queries
{
    public record GetUsersByOrganization(
        Guid OrganizationId
    );
}

namespace RbacService.Domain.Entities
{
    public class Organization : BaseEntity
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; } = default!;
        public string? Type { get; set; }
        public string? Description { get; set; }
        public Guid? ParentOrganizationId { get; set; }
        public Organization? ParentOrganization { get; set; }

        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }

}

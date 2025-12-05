using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class DepartmentRepository(RbacDbContext context) : GenericRepository<Department>(context), IDepartmentRepository
    {
        public async Task<IEnumerable<Department>> GetDepartmentsByOrganizationAsync(Guid organizationId)
        {
            return await _context.Departments
                .Where(d => d.OrganizationId == organizationId)
                .ToListAsync();
        }
    }
}

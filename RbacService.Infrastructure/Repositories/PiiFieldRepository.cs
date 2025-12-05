using Microsoft.EntityFrameworkCore;
using RbacService.Domain.Entities;
using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.Repositories
{
    public class PiiFieldRepository(RbacDbContext context) : GenericRepository<PiiField>(context), IPiiFieldRepository
    {
        public async Task<PiiField?> GetByCodeAsync(string code)
        {
            return await _context.PiiFields
                .FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}

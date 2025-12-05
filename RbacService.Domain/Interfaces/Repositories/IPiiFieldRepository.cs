using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IPiiFieldRepository : IGenericRepository<PiiField>
    {
        Task<PiiField?> GetByCodeAsync(string code);
    }
}

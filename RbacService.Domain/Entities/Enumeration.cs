using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class Enumeration : BaseEntity
    {
        public Guid EnumerationId { get; set; }
        public string Type { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Value { get; set; } = default!;
        public string? Description { get; set; }
    }

}

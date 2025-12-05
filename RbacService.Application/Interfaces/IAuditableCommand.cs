using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Application.Interfaces
{
    public interface IAuditableCommand
    {
        string? UserEmail { get; set; }
    }
}

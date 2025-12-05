using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Application.DTOs
{
    public record UserDeleteDto(
        Guid UserId
    );
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Application.DTOs.User
{
    public record UserDeleteDto(
        Guid UserId
    );
}

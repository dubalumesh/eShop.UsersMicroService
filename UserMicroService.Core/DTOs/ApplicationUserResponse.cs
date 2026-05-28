using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMicroService.Core.DTOs
{

    public record ApplicationUserResponse(
        int Id,
        string Email,
        string FirstName,
        string LastName,
        string Gender,
        string DisplayName
    )
    {
        public ApplicationUserResponse() : this(default, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }


    }
}

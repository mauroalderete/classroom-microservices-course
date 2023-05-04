using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationRole Role { get; set; }
        public ApplicationUser User { get; set; }
    }
}

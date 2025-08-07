using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.ENTITIES.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string NormalizedFullName { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}

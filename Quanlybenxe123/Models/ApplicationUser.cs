using Microsoft.AspNetCore.Identity;

namespace Quanlybenxe123.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        
    }
}

using Microsoft.AspNetCore.Identity;

namespace FirstApii.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}

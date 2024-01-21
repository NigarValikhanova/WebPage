using MainWebApp.StaticData;
using Microsoft.AspNetCore.Identity;

namespace MainWebApp.Models
{
    public class AppUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public bool Gender { get; set; }
        //public UserRole Role {  get; set; }
    }
}

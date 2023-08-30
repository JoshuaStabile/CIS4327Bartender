using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CIS4327_Bartender.Models.Data
{
    public class AppUser : IdentityUser<Guid>
    {
        /*
        [EmailAddress]
        public string Username { get; set; }
        
        [PasswordPropertyText]
        public string Password { get; set; }
        */
    }
}

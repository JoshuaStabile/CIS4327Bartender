using System.ComponentModel.DataAnnotations;

namespace CIS4327_Bartender.Models.Account
{
    public class LoginAccountModel
    {
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}

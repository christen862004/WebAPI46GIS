using System.ComponentModel.DataAnnotations;

namespace WebAPI46GIS.DTO
{
    public class RegisterUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}

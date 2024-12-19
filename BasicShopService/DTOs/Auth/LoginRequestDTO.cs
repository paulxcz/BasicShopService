using System.ComponentModel.DataAnnotations;

namespace BasicShopService.DTOs.Auth
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

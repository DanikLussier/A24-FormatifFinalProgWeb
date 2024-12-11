using System.ComponentModel.DataAnnotations;

namespace DixCordesServeur.Models.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string PasswordConfirm { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace CommercialShop.Models.VerifyEmailDTOs
{
    public class VerifyEmail
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace CommercialShop.Models.ChangePasswordDTOs
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Less tha 40, more than 8")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password does not match")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmNewPassword { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;

    }
}

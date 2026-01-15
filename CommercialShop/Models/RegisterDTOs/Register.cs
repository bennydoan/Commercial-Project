using System.ComponentModel.DataAnnotations;

namespace CommercialShop.Models.RegisterDTOs
{
    public class Register
    {
        [Required(ErrorMessage ="Name is required")]
        [Display(Name ="Full Name")]
        public string Name {  get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Day of Birth")]
        [Required(ErrorMessage = "Date is required")]
        [CustomValidation(typeof(DateValidator), nameof(DateValidator.PastDate))]
        public DateTime DOB {  get; set; }

        [DataType(DataType.Password)]
        [StringLength(40,MinimumLength =8,ErrorMessage ="Less tha 40, more than 8")]
        [Compare("ConfirmPassword",ErrorMessage ="Password does not match")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


        [CustomValidation(typeof(MustBeTrue), nameof(MustBeTrue.MustBeTrueResult))]
        public bool AgreeTerms {  get; set; }


    }
    //custome validation
    public class DateValidator
    {
        public static ValidationResult PastDate(DateTime value, ValidationContext context)
        {
            if (value < DateTime.Today)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Date must be in the past.");
        }
    }


    // must be true validation 

    public class MustBeTrue
    {
        public static ValidationResult MustBeTrueResult( bool value , ValidationContext context)
        {
            if(value == true)
            {
                return ValidationResult.Success;

            }
            return new ValidationResult("This field must be agreed");
        }
    }


}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CustMgmSys.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }
    }
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        public string? Role { get; set; }

    }
    public class Status
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
    public class Customer
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter a date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }



    }

}

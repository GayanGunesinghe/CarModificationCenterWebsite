//structure for the user data base for registrations and login.
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarModificationCenter.Models
{
    public class website_users
    {
        [Key] public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public int Mobile { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        [DisplayName("Profile Picture")]
        public byte[] DisplayPicture { get; set; }
    }

    //registration model
    public class RegistrationViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Profile Picture")]
        public byte[] DisplayPicture { get; set; }
    }

    //login model
    public class LoginViewModel
    {
        [Key] public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UserDetailsViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public int Mobile { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }
    }
}
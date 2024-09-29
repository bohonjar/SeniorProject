using System.ComponentModel.DataAnnotations; // Importing necessary namespace for data annotations

namespace SeniorProject.Models // Defining the namespace for the Models
{
    public class RegistrationModel // Defining the RegistrationModel class
    {
        [Required(ErrorMessage = "First name is required.")] // Ensures first name is provided
        public string FirstName { get; set; } // Property to store the user's first name

        [Required(ErrorMessage = "Last name is required.")] // Ensures last name is provided
        public string LastName { get; set; } // Property to store the user's last name

        [Required(ErrorMessage = "Sex is required.")] // Ensures sex is selected
        public string Sex { get; set; } // Property to store the user's sex (gender)

        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100.")] // Validates age range
        public int Age { get; set; } // Property to store the user's age

        [Required(ErrorMessage = "State is required.")] // Ensures state is selected
        public string State { get; set; } // Property to store the user's state of residence

        [Required(ErrorMessage = "Email is required.")] // Ensures email is provided
        [EmailAddress(ErrorMessage = "Invalid email address format.")] // Validates email format
        public string Email { get; set; } // Property to store the user's email address

        [Required(ErrorMessage = "Username is required.")] // Ensures username is provided
        public string UserName { get; set; } // Property to store the user's chosen username

        [Required(ErrorMessage = "Password is required.")] // Ensures password is provided
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")] // Validates password length
        public string Password { get; set; } // Property to store the user's password
    }
}
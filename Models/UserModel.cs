using System.ComponentModel.DataAnnotations; // Importing necessary namespace for data annotations

namespace SeniorProject.Models // Defining the namespace for the Models
{
    public class UserModel // Defining the UserModel class
    {
        public int Id { get; set; } // Property to store the user's unique identifier

        [Required(ErrorMessage = "Username is required.")] // Ensures username is provided
        public string UserName { get; set; } // Property to store the user's username

        [Required(ErrorMessage = "Password is required.")] // Ensures password is provided
        public string Password { get; set; } // Property to store the user's password
    }
}
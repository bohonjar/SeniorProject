namespace SeniorProject.Models // Defining the namespace for the Models
{
    public class RegistrationModel // Defining the RegistrationModel class
    {
        public string FirstName { get; set; } // Property to store the user's first name
        public string LastName { get; set; } // Property to store the user's last name
        public string Sex { get; set; } // Property to store the user's sex (gender)
        public int Age { get; set; } // Property to store the user's age
        public string State { get; set; } // Property to store the user's state of residence
        public string Email { get; set; } // Property to store the user's email address
        public string UserName { get; set; } // Property to store the user's chosen username
        public string Password { get; set; } // Property to store the user's password
    }
}
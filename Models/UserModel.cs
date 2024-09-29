namespace SeniorProject.Models // Defining the namespace for the Models
{
    public class UserModel // Defining the UserModel class
    {
        public int Id { get; set; } // Property to store the user's unique identifier
        public string UserName { get; set; } // Property to store the user's username
        public string Password { get; set; } // Property to store the user's password
    }
}
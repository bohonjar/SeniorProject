using SeniorProject.Models; // Importing the Models namespace for the project
using SeniorProject.Services; // Importing the Services namespace for the project

public class SecurityService // Defining the SecurityService class for handling security-related operations
{
    private readonly SecurityDAO _securityDAO; // Declaring a SecurityDAO instance for database operations

    // Constructor to initialize the SecurityDAO
    public SecurityService(SecurityDAO securityDAO)
    {
        _securityDAO = securityDAO; // Assigning the SecurityDAO to the private field
    }

    // Method to validate a user's credentials
    public bool IsValid(UserModel user)
    {
        return _securityDAO.FindUserByNameAndPassword(user); // Calling the DAO method to check if the user exists
    }

    // Method to save a new user to the database
    public void SaveUser(RegistrationModel model)
    {
        _securityDAO.InsertUser(model); // Calling the DAO method to insert the new user
    }

    // Method to check if a username already exists
    public bool UsernameExists(string username)
    {
        return _securityDAO.CheckUsernameExists(username); // Calling the DAO method to check for existing usernames
    }

    // Method to check if an email already exists
    public bool EmailExists(string email)
    {
        return _securityDAO.CheckEmailExists(email); // Calling the DAO method to check for existing emails
    }
}
using SeniorProject.Models; // Importing the Models namespace for the project
using System; // Importing system functionalities
using Microsoft.Data.SqlClient; // Importing SQL Server functionalities
using BCrypt.Net; // Importing BCrypt for password hashing and verification

namespace SeniorProject.Services // Defining the namespace for the Services
{
    public class SecurityDAO // Defining the SecurityDAO class for database operations related to security
    {
        // Connection string to connect to the SQL Server database
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SeniorProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // Method to find a user by username and password
        public bool FindUserByNameAndPassword(UserModel user)
        {
            bool success = false; // Variable to indicate if the login was successful

            string sqlStatement = "SELECT password FROM dbo.Users WHERE username = @username"; // SQL query to get the hashed password

            using (SqlConnection connection = new SqlConnection(connectionString)) // Using statement for automatic resource management
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection); // Creating SQL command
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = user.UserName; // Adding username parameter

                try
                {
                    connection.Open(); // Opening the database connection
                    string hashedPassword = (string)command.ExecuteScalar(); // Executing the command and retrieving the hashed password

                    // Verify the password
                    if (hashedPassword != null && BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword)) // Checking if the provided password matches the stored hashed password
                        success = true; // Set success to true if passwords match
                }
                catch (Exception ex) // Catching any exceptions that occur
                {
                    Console.WriteLine(ex.Message); // Logging the exception message
                }
            }

            return success; // Returning the success status
        }

        // Method to check if a username already exists in the database
        public bool CheckUsernameExists(string username)
        {
            string sqlStatement = "SELECT COUNT(1) FROM dbo.RegisteredUsers WHERE UserName = @UserName"; // SQL query to count users with the given username

            using (SqlConnection connection = new SqlConnection(connectionString)) // Using statement for automatic resource management
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection); // Creating SQL command
                command.Parameters.AddWithValue("@UserName", username); // Adding username parameter

                connection.Open(); // Opening the database connection
                return (int)command.ExecuteScalar() > 0; // Returning true if at least one user exists with that username
            }
        }

        // Method to check if an email already exists in the database
        public bool CheckEmailExists(string email)
        {
            string sqlStatement = "SELECT COUNT(1) FROM dbo.RegisteredUsers WHERE Email = @Email"; // SQL query to count users with the given email

            using (SqlConnection connection = new SqlConnection(connectionString)) // Using statement for automatic resource management
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection); // Creating SQL command
                command.Parameters.AddWithValue("@Email", email); // Adding email parameter

                connection.Open(); // Opening the database connection
                return (int)command.ExecuteScalar() > 0; // Returning true if at least one user exists with that email
            }
        }

        // Method to insert a new user into the database
        public void InsertUser(RegistrationModel model)
        {
            string sqlStatementUser = "INSERT INTO dbo.Users (username, password) VALUES (@UserName, @Password)"; // SQL query for inserting a user
            string sqlStatementRegisteredUser = "INSERT INTO dbo.RegisteredUsers (FirstName, LastName, Sex, Age, State, Email, UserName, Password) " +
                                                "VALUES (@FirstName, @LastName, @Sex, @Age, @State, @Email, @UserName, @Password)"; // SQL query for inserting a registered user

            using (SqlConnection connection = new SqlConnection(connectionString)) // Using statement for automatic resource management
            {
                SqlTransaction? transaction = null; // Declaring a transaction variable

                try
                {
                    connection.Open(); // Opening the database connection
                    transaction = connection.BeginTransaction(); // Starting a transaction

                    // Hash the password before storing it
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password); // Hashing the user's password

                    // Insert user
                    SqlCommand commandUser = new SqlCommand(sqlStatementUser, connection, transaction); // Creating SQL command for user insertion
                    commandUser.Parameters.AddWithValue("@UserName", model.UserName); // Adding username parameter
                    commandUser.Parameters.AddWithValue("@Password", hashedPassword); // Adding hashed password parameter
                    commandUser.ExecuteNonQuery(); // Executing the insert command

                    // Insert registered user
                    SqlCommand commandRegisteredUser = new SqlCommand(sqlStatementRegisteredUser, connection, transaction); // Creating SQL command for registered user insertion
                    commandRegisteredUser.Parameters.AddWithValue("@FirstName", model.FirstName); // Adding first name parameter
                    commandRegisteredUser.Parameters.AddWithValue("@LastName", model.LastName); // Adding last name parameter
                    commandRegisteredUser.Parameters.AddWithValue("@Sex", model.Sex); // Adding sex parameter
                    commandRegisteredUser.Parameters.AddWithValue("@Age", model.Age); // Adding age parameter
                    commandRegisteredUser.Parameters.AddWithValue("@State", model.State); // Adding state parameter
                    commandRegisteredUser.Parameters.AddWithValue("@Email", model.Email); // Adding email parameter
                    commandRegisteredUser.Parameters.AddWithValue("@UserName", model.UserName); // Adding username parameter
                    commandRegisteredUser.Parameters.AddWithValue("@Password", hashedPassword); // Adding hashed password parameter
                    commandRegisteredUser.ExecuteNonQuery(); // Executing the insert command for registered user

                    transaction.Commit(); // Committing the transaction
                }
                catch (Exception ex) // Catching any exceptions that occur
                {
                    Console.WriteLine(ex.Message); // Logging the exception message
                    transaction?.Rollback(); // Rolling back the transaction if an error occurs
                }
            }
        }
    }
}
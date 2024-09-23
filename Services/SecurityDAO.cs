using SeniorProject.Models;
using System;
using Microsoft.Data.SqlClient;
using BCrypt.Net;

namespace SeniorProject.Services
{
    public class SecurityDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SeniorProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public bool FindUserByNameAndPassword(UserModel user)
        {
            bool success = false;

            string sqlStatement = "SELECT password FROM dbo.Users WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = user.UserName;

                try
                {
                    connection.Open();
                    string hashedPassword = (string)command.ExecuteScalar();

                    // Verify the password
                    if (hashedPassword != null && BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword))
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return success;
        }

        public bool CheckUsernameExists(string username)
        {
            string sqlStatement = "SELECT COUNT(1) FROM dbo.RegisteredUsers WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@UserName", username);

                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }

        public bool CheckEmailExists(string email)
        {
            string sqlStatement = "SELECT COUNT(1) FROM dbo.RegisteredUsers WHERE Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }

        public void InsertUser(RegistrationModel model)
        {
            string sqlStatementUser = "INSERT INTO dbo.Users (username, password) VALUES (@UserName, @Password)";
            string sqlStatementRegisteredUser = "INSERT INTO dbo.RegisteredUsers (FirstName, LastName, Sex, Age, State, Email, UserName, Password) " +
                                                "VALUES (@FirstName, @LastName, @Sex, @Age, @State, @Email, @UserName, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction? transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    // Hash the password before storing it
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                    // Insert user
                    SqlCommand commandUser = new SqlCommand(sqlStatementUser, connection, transaction);
                    commandUser.Parameters.AddWithValue("@UserName", model.UserName);
                    commandUser.Parameters.AddWithValue("@Password", hashedPassword);
                    commandUser.ExecuteNonQuery();

                    // Insert registered user
                    SqlCommand commandRegisteredUser = new SqlCommand(sqlStatementRegisteredUser, connection, transaction);
                    commandRegisteredUser.Parameters.AddWithValue("@FirstName", model.FirstName);
                    commandRegisteredUser.Parameters.AddWithValue("@LastName", model.LastName);
                    commandRegisteredUser.Parameters.AddWithValue("@Sex", model.Sex);
                    commandRegisteredUser.Parameters.AddWithValue("@Age", model.Age);
                    commandRegisteredUser.Parameters.AddWithValue("@State", model.State);
                    commandRegisteredUser.Parameters.AddWithValue("@Email", model.Email);
                    commandRegisteredUser.Parameters.AddWithValue("@UserName", model.UserName);
                    commandRegisteredUser.Parameters.AddWithValue("@Password", hashedPassword); // Use hashed password
                    commandRegisteredUser.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction?.Rollback();
                }
            }
        }
    }
}
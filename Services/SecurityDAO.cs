using SeniorProject.Models;
using System;
using Microsoft.Data.SqlClient;

namespace SeniorProject.Services
{
    public class SecurityDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SeniorProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public bool FindUserByNameAndPassword(UserModel user)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @username and password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = user.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        success = true;

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return success;
        }

        public void InsertUser(RegistrationModel model)
        {
            string sqlStatementUser = "INSERT INTO dbo.Users (username, password) VALUES (@UserName, @Password)";
            string sqlStatementRegisteredUser = "INSERT INTO dbo.RegisteredUsers (FirstName, LastName, Sex, Age, State, Email, UserName, Password) " +
                                                "VALUES (@FirstName, @LastName, @Sex, @Age, @State, @Email, @UserName, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    SqlCommand commandUser = new SqlCommand(sqlStatementUser, connection, transaction);
                    commandUser.Parameters.AddWithValue("@UserName", model.UserName);
                    commandUser.Parameters.AddWithValue("@Password", model.Password);
                    commandUser.ExecuteNonQuery();

                    SqlCommand commandRegisteredUser = new SqlCommand(sqlStatementRegisteredUser, connection, transaction);
                    commandRegisteredUser.Parameters.AddWithValue("@FirstName", model.FirstName);
                    commandRegisteredUser.Parameters.AddWithValue("@LastName", model.LastName);
                    commandRegisteredUser.Parameters.AddWithValue("@Sex", model.Sex);
                    commandRegisteredUser.Parameters.AddWithValue("@Age", model.Age);
                    commandRegisteredUser.Parameters.AddWithValue("@State", model.State);
                    commandRegisteredUser.Parameters.AddWithValue("@Email", model.Email);
                    commandRegisteredUser.Parameters.AddWithValue("@UserName", model.UserName);
                    commandRegisteredUser.Parameters.AddWithValue("@Password", model.Password);
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
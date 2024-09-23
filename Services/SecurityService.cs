using SeniorProject.Models;
using SeniorProject.Services;

public class SecurityService
{
    private readonly SecurityDAO _securityDAO;

    public SecurityService(SecurityDAO securityDAO)
    {
        _securityDAO = securityDAO;
    }

    public bool IsValid(UserModel user)
    {
        return _securityDAO.FindUserByNameAndPassword(user);
    }

    public void SaveUser(RegistrationModel model)
    {
        _securityDAO.InsertUser(model);
    }

    public bool UsernameExists(string username)
    {
        return _securityDAO.CheckUsernameExists(username);
    }

    public bool EmailExists(string email)
    {
        return _securityDAO.CheckEmailExists(email);
    }
}
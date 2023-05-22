using UserApi.Models;

namespace UserApi.Data;

public interface IUserRepository
{
    Task<User> GetUser(string username, string password);
}
using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data;

public class UserRepository : IUserRepository
{
    private readonly UserApiContext context;
    public UserRepository(UserApiContext ctx)
    {
        context = ctx;
    }

    public async Task<User> GetUser(string username, string password)
    {
        // User? getUser = await context.Users.Where(user => user.username == username && user.password == password).FirstOrDefault();
        User? getUser = await context.Users.FirstOrDefaultAsync(user => user.username == username && user.password == password);
        if (getUser is null) return null;
        await context.Entry(getUser).ReloadAsync();
        return getUser;
    }
    
}
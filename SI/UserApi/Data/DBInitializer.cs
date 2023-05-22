using UserApi.Models;

namespace UserApi.Data;

public class DBInitializer : IDBInitializer
{
    public void initialize(UserApiContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        if (context.Users.Any())
        {
            return;   // DB has been seeded
        }

        List<User> users = new List<User>
        {
            new User{id = 1, username = "faustas",password = "faustas123"},
            new User{id = 2,username = "rolf",password = "rolf123"},
            new User{id = 3,username = "kamilla",password = "kamilla123"},
            new User{id = 4,username = "lazlo",password = "lazlo123"},
        };
        

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}
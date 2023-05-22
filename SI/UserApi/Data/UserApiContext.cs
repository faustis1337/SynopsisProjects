using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data;

public class UserApiContext : DbContext
{
    public UserApiContext(DbContextOptions<UserApiContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
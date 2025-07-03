using ProductCategoryAPI.Data;
using ProductCategoryAPI.Models;

public class DataSeeder
{
    private readonly AppDbContext _context;
    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedUsersAsync()
    {
        var users = new List<User>();
        /*for (int i = 1; i <= 1000; i++)
        {
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                Username = $"user{i}",
                Password = $"pass{i}",
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                Role = "User",
                IsActive = true
            });
        }*/

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();
    }
}

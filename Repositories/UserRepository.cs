using CsharpTestApp.Models;

namespace CsharpTestApp.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public IEnumerable<User> GetAll() => _users;

    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public void Add(User user)
    {
        user.Id = _users.Count + 1;
        _users.Add(user);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        return Task.FromResult(user);
    }
}
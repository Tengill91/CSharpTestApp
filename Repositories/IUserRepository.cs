using CsharpTestApp.Models;

namespace CsharpTestApp.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User? GetById(int id);
    void Add(User user);
    Task<User?> GetByEmailAsync(string email);
}
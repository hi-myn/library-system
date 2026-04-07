using library_system.Interfaces;
using library_system.Models;
using System.Linq;

namespace library_system.Services;

public class UserService
{
    private readonly IRepository<User> _repo;
    public UserService(IRepository<User> repo)
    {
        _repo = repo;
    }

    public User? FindById(string id)
    {
        var user = _repo.GetById(id);
        return user;
    }

    public void AddUser(User user)
    {
        _repo.Add(user);
        System.Console.WriteLine($"User '{user.Name}' registered successfully");
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _repo.GetAll();
    }

    public IEnumerable<User> Search(string term)
    {
        term = term.ToLower();
        return _repo.GetAll().Where(b =>
        b.Name.ToLower().Contains(term) ||
        b.Email.ToLower().Contains(term));
    }

}

using backend.Models;

namespace backend.Repositories
{
    public interface IUserRepository
    {
        Task<int> Register(User user);
       
       Task<User?> GetByUsername(string username); 
    }
}

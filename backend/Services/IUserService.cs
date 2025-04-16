using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        string CreateToken(User user);

        Task<int> Register(UserRequest request); 
        Task<object> Login(UserRequest request);
        int Authorize(string token); 
    }
}

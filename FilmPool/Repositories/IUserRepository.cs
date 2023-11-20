using FilmPool.DbModels;
using FilmPool.RequestModels;

namespace FilmPool.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get();
        Task<User> AuthenticateUser(string userName, string password);
        Task<User> Get(int id);
        Task<bool> Create(User user);
        Task<bool> Update(UserUpdateRequestModel user);
        Task<User> Delete(int id);
        string GetRole(User user);
        Task<User> FindUserByEmail(string email);
        string GenerateToken(User user, long ticks);
        string GetHashedPassword(string password);
        Task<bool> ResetPasswordAsync(User user, string token, string newPassword);
        bool IsTokenValid(string token, User user);
        Task<bool> BlockUser(int Id);
  
  }
}

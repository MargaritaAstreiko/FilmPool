using FilmPool.DbModels;
using FilmPool.RequestModels;

namespace FilmPool.Services
{
  public interface IUserService
  {
    Task<IEnumerable<User>> Get();
    Task<User> Get(int id);
    Task<User> AuthenticateUser(string userName, string password);
    Task<bool> Create(User user);
    Task<bool> Update(UserUpdateRequestModel user);
    Task<User> Delete(int id);
    Task<User> FindUserByEmail(string email);
    string GenerateToken(User user, long ticks);
    string GetHashedPassword(string password);
    Task<bool> ResetPasswordAsync(User user, string token, string newPassword);
    Task<bool> BlockUser(int id);

  }
}

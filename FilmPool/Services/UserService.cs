using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.Repositories;
using FilmPool.RequestModels;

namespace FilmPool.Services
{
  public class UserService: IUserService
  {
    private readonly IUserRepository _userRepository;


    public UserService(IUserRepository context)
    {
      _userRepository = context;
    }

    public async Task<IEnumerable<User>> Get()
    {
      return await _userRepository.Get();
    }

    public async Task<User> AuthenticateUser(string userName, string password)
    {
      return await _userRepository.AuthenticateUser(userName, password);
    }

    public async Task<User> FindUserByEmail(string email)
    {
      return await _userRepository.FindUserByEmail(email);
    }

    public async Task<User> Get(int id)
    {
      return await _userRepository.Get(id);
    }
    public async Task<bool> Update(UserUpdateRequestModel user)
    {
      return await _userRepository.Update(user);
    }

    public async Task<User> Delete(int id)
    {
      return await _userRepository.Delete(id);
    }

    public async Task<bool> Create(User user)
    {
      return await _userRepository.Create(user);

    }

    public string GenerateToken(User user, long ticks)
    {
      return _userRepository.GenerateToken(user, ticks);
    }

    public  string GetHashedPassword(string password)
    {
      return _userRepository.GetHashedPassword(password);
    }

    public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword)
    {
      return await _userRepository.ResetPasswordAsync(user, token, newPassword);
    }

  }
}

using FilmPool.Data;
using FilmPool.DbModels;
using FilmPool.RequestModels;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace FilmPool.Repositories
{
  public class UserRepository : IUserRepository
  {
    private FilmPoolDbContext Context;
    private const string _alg = "HmacSHA256";
    private const string _salt = "rz8Lugtrnetgh";
    private const int _expirationMinutes =2880;

    public UserRepository(FilmPoolDbContext context)
    {
      Context =  context;
    }
    
    public async Task <IEnumerable<User>> Get()
    {
       return await Context.Users.ToListAsync();
    }

    public string GetRole(User user)
    {
      switch ((int)user.UserRole) {
        case 0:
        return "User";
        case 1:
        return "Admin";
        default:
        return "";
      }
  
    }

    public async Task<User> Get(int Id)
    {
      return await Context.Users.FindAsync(Id);
    }

    public async Task<User> AuthenticateUser(string userName, string password)
    {
  
      var user = await Context.Users.Where(x=> x.UserName==userName).FirstOrDefaultAsync();
      if (user != null)
      {
        user = user.Password == password? user : null;
      }
      return user;
    }

    public async Task<User> FindUserByEmail(string email)
    {

      var user = await Context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
      return user;
    }

    public async Task<bool> Create(User user)
    {
      Context.Users.Add(user);
      await Context.SaveChangesAsync();
      return true;
    }
    public async Task <bool> Update(UserUpdateRequestModel user)
    {
      User currentUser = await Get(user.Id);
      currentUser.FirstName= user.FirstName;
      currentUser.LastName= user.LastName;
      currentUser.UserName = user.UserName;
      currentUser.Email= user.Email;
      //currentUser.Password= user.Password;
      //currentUser.UserRole = user.UserRole;

      Context.Users.Update(currentUser);
      await Context.SaveChangesAsync();
      return true;

    }

    public async Task<User> Delete(int Id)
    {
      User user = await Get(Id);

      if (user != null)
      {
        Context.Users.Remove(user);
        await Context.SaveChangesAsync();
      }

      return user;
    }

    public string GenerateToken(User user, long ticks)
    {

    string hash = string.Join(":", new string[] { user.UserName});
      string hashLeft = "";
      string hashRight = "";
      DateTime now = DateTime.Now;
      ticks = ticks > 0 ? ticks : now.Ticks;

      using (HMAC hmac = HMACSHA256.Create(_alg))
      {
        hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(user.Password));
        hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));

        hashLeft = Convert.ToBase64String(hmac.Hash);
        hashRight = string.Join(":", new string[] { user.UserName, ticks.ToString() });
      }

      return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
    }

    public  string GetHashedPassword(string password)
    {
      string key = string.Join(":", new string[] { password, _salt });

      using (HMAC hmac = HMACSHA256.Create(_alg))
      {
        // Hash the key.
        hmac.Key = Encoding.UTF8.GetBytes(_salt);
        hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

        return Convert.ToBase64String(hmac.Hash);
      }
    }

    public  bool IsTokenValid(string token, User user)
    {
      bool result = false;

      try
      {
        string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));

        string[] parts = key.Split(new char[] { ':' });
        if (parts.Length == 3)
        {

          string hash = parts[0];
          string username = parts[1];
          long ticks = long.Parse(parts[2]);
          DateTime timeStamp = new DateTime(ticks);

          bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > _expirationMinutes;
          if (!expired)
          {
    
            if (username == user.UserName)
            {

              string computedToken = GenerateToken(user, ticks);

  
              result = (token == computedToken);
            }
          }
        }
      }
      catch
      {
      }

      return result;
    }
    public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword )
    {
      bool IsValid = IsTokenValid(token, user);
      if(IsValid)
      {
        User currentUser = await Get(user.Id);
        currentUser.FirstName = user.FirstName;
        currentUser.LastName = user.LastName;
        currentUser.UserName = user.UserName;
        currentUser.Email = user.Email;
        currentUser.Password = newPassword;
        currentUser.UserRole = user.UserRole;

        Context.Users.Update(currentUser);
        await Context.SaveChangesAsync();
      }
      return IsValid;
    }
  }
}

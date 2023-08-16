using FilmPool.DbModels;
using FilmPool.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace FilmPool.JWT
{
  public class AuthService
  {
    
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;
    private readonly IUserRepository _userRepository;
   

    public AuthService(IConfiguration configuration, IUserRepository context)
    {
      _configuration = configuration;
      _jwtSettings = _configuration.GetSection("JwtSettings");
      _userRepository = context;
  
    }
    public SigningCredentials GetSigningCredentials()
    {
      var key = Encoding.UTF8.GetBytes("MySuperSecretKey@12345");
      var secret = new SymmetricSecurityKey(key);
      return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    public List<Claim> GetClaims(User user) 
     
    {
      string role = _userRepository.GetRole(user);
      var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, role),
        };

      return claims;
    }

    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    { 

      var tokenOptions = new JwtSecurityToken(
          issuer: _jwtSettings["validIssuer"],
          audience: _jwtSettings["validAudience"],
          claims: claims,
          expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
          signingCredentials: signingCredentials);
      return tokenOptions;
    }

   
  }
}

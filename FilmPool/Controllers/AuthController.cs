using AutoMapper;
using FilmPool.DbModels;
using FilmPool.JWT;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
using FilmPool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;

namespace FilmPool.Controllers
{
  //"code-maze.com/user-lockout-functionality-with-angular-and-asp-net-core-identity/"

  [Route("api/")]
    [ApiController]
    public class AuthController : Controller
    {
    private readonly AuthService _jwtHandler;
    private readonly IMapper _mapper;
    IUserService _userService;
    private readonly IEmailSender _emailSender;

    public AuthController(IMapper mapper, AuthService jwtHandler, IUserService userService,  IEmailSender emailSender)
    {
      _jwtHandler = jwtHandler;
      _mapper = mapper;
      _userService = userService;
      _emailSender = emailSender;
    
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel userForAuthentication)
    {
      var user = await _userService.AuthenticateUser(userForAuthentication.UserName, userForAuthentication.Password);
      if (user == null)
        return Unauthorized(new LoginResponse { ErrorMessage = "Неверный логин или пароль" });
      var signingCredentials = _jwtHandler.GetSigningCredentials();
      var claims = _jwtHandler.GetClaims(user);
      var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
      var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
      return Ok(new LoginResponse { IsAuthSuccessful = true, Token = token, User=user });
    }

      [HttpPost("registration")]
      public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationModel userForRegistration)
      {
        if (userForRegistration == null || !ModelState.IsValid)
          return BadRequest();

        var user = _mapper.Map<User>(userForRegistration);

        var result = await _userService.Create(user);
        if (!result)
        {
        //  var errors = result.Errors.Select(e => e.Description);
          return BadRequest(new RegisterResponseModel { Errors = "Не удалось зарегистрировать пользователя"});
        }

        return StatusCode(201);
      }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPasswordModel)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      var user = await _userService.FindUserByEmail(forgotPasswordModel.Email);
      if (user == null)
        return BadRequest("Invalid Request");

      var token =  _userService.GenerateToken(user,0);
      var param = new Dictionary<string, string?>
    {
        {"token", token },
        {"email", forgotPasswordModel.Email }
    };

      var callback = QueryHelpers.AddQueryString(forgotPasswordModel.ClientURI, param);
      var message = new Message(new string[] { user.Email }, "Reset password token", callback);
      _emailSender.SendEmail(message);

      return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPassword)
    {
      if (!ModelState.IsValid)
        return BadRequest();
      var user = await _userService.FindUserByEmail(resetPassword.Email);
      if (user == null)
        return BadRequest("Invalid Request");
      var resetPassResult = await _userService.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
      if (!resetPassResult)
      {

        return BadRequest("Invalid Token");
      }
      return Ok();
    }
  }
  
}

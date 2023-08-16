using FilmPool.DbModels;

namespace FilmPool.ResponseModels
{
  public class LoginResponse
  {
    public bool IsAuthSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
    public User? User { get; set;}
  }
}

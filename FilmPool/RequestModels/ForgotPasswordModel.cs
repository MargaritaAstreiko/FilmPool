using System.ComponentModel.DataAnnotations;

namespace FilmPool.RequestModels
{
  public class ForgotPasswordModel
  {
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? ClientURI { get; set; }
  }
}

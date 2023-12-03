using System.ComponentModel.DataAnnotations;

public class LoginModel
{
    [Required(ErrorMessage = "Email is required.")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
    public bool? isBlocked { get; set; }
}

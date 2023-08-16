
namespace FilmPool.DbModels
{
  public enum RoleEnum
  {
    User=0,
    Admin=1
  }

  public class Role
  { 
    public RoleEnum Id { get; set; }
    public string RoleName { get; set; }
  }
}

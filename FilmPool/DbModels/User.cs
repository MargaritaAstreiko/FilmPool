using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FilmPool.DbModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[]? Picture { get; set; }
        public Boolean IsBlocked { get; set; }
        public RoleEnum UserRole { get; set; }
        public Role Role { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Collections> Collections { get; set; }
    }
}

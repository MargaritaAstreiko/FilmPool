using FilmPool.DbModels;

namespace FilmPool.ResponseModels
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Picture { get; set; }
        public RoleEnum UserRole { get; set; }


    }
}

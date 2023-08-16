using AutoMapper;
using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;

namespace FilmPool.JWT
{
    public class UserProfile : Profile
    {
      public UserProfile()
      {

        CreateMap<User, LoginModel>();
        CreateMap<UserRegistrationModel, User>();
        CreateMap<UserRegistrationModel, User>()
        .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email)); 
    }
  }
}

using AutoMapper;
using FilmPool.DbModels;
using FilmPool.RequestModels;

namespace FilmPool.Profilies
{
    public class CollectionProfile: Profile
    {
        public CollectionProfile()
        {

            CreateMap<CollectionRequestModel, Collections>()
            .ForMember(dest => dest.User, o => o.MapFrom(src => new User { Id = src.UserId }));
        }


    }
}

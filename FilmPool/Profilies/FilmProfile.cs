using AutoMapper;
using FilmPool.DbModels;
using FilmPool.RequestModels;

namespace FilmPool.Profilies
{
    public class FilmProfile : Profile
    {
        public FilmProfile()
        {

            CreateMap<FilmUpdateRequestModel, Film>()
            .ForMember(dest => dest.FilmGenre, o => o.MapFrom(src => new Genre { Id = src.Genre }));


        }


    }
}

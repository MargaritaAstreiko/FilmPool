using FilmPool.DbModels;

namespace FilmPool.ResponseModels
{
  public class FilmsResponseModel
  {
    public List<FilmModel>? films { get; set; }
    public int totalFilms { get; set; }
  }
}

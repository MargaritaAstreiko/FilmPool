using FilmPool.DbModels;

namespace FilmPool.RequestModels
{
  public class FilmsRequestModel
  {
    public int currentPage { get; set; }
    public int pageSize { get; set; }
    public string? search { get; set; }
    public Genre? genre { get; set;}
  }
}

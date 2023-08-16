using FilmPool.DbModels;

namespace FilmPool.RequestModels
{
  public class FilmUpdateRequestModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int? Year { get; set; }
    public string? Description { get; set; }
  }
}

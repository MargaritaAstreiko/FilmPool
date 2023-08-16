using FilmPool.DbModels;
using System.Drawing;

namespace FilmPool.ResponseModels
{
  public class FilmModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public GenreEnum Genre { get; set; }
    public Genre FilmGenre { get; set; }
    public int? Year { get; set; }
    public string Duration { get; set; }
    public string? Description { get; set; }
    public string? Picture { get; set; }
    public double? Rating { get; set; }

  }
}

using FilmPool.DbModels;

namespace FilmPool.RequestModels
{
    public class FilmUpdateRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public GenreEnum Genre { get; set; }
        public string Duration { get; set; }
        public int? Year { get; set; }
        public string? Description { get; set; }
        public string? FilmUrl { get; set; }
    }
}

namespace FilmPool.DbModels
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public GenreEnum Genre { get; set; }
        public Genre FilmGenre { get; set; }
        public int? Year { get; set; }
        public string Duration { get; set; }
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
        public double? TotalRating { get; set; }
        public string? FilmUrl { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<FilmsInCollections> Collections { get; set; }

    }
}

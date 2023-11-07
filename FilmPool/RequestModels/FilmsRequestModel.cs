using FilmPool.DbModels;

namespace FilmPool.RequestModels
{
    public class FilmsRequestModel
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int year { get; set; }
        public string? search { get; set; }
        public Genre? genre { get; set; }
        public bool? rating { get; set; }
    }
}

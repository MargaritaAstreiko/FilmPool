using FilmPool.DbModels;

namespace FilmPool.ResponseModels
{
    public class CommentsResponseModel
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public string CreatedDate { get; set; }
        public Film Film { get; set; }
        public User User { get; set; }
    }
}

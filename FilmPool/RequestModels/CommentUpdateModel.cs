namespace FilmPool.RequestModels
{
    public class CommentUpdateModel
    {   
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

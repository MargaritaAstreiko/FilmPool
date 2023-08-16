namespace FilmPool.RequestModels
{
  public class CommentRequestModel
  {
    public int FilmId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}

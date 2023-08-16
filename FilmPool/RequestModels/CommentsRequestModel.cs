namespace FilmPool.RequestModels
{
  public class CommentsRequestModel
  {
    public int currentPage { get; set; }
    public int pageSize { get; set; }
    public int filmId { get; set; }
  }
}

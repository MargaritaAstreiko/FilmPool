namespace FilmPool.RequestModels
{
  public class RatingRequestModel
  {
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
  }
}

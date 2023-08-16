namespace FilmPool.DbModels
{
  public class Rating
  {
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    public Film Film { get; set; }
    public User User { get; set; }

  }
}

namespace FilmPool.ResponseModels
{
  public class RatingResponseModel
  {
    public RatingResponseModel(int id, int score)
    {
      Id = id;
      Score = score;
    }

    public int Id { get; set; }
    public int  Score { get; set; }

  }
}

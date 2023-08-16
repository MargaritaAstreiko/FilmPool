namespace FilmPool.RequestModels
{
  public class CollectionRequestModel
  {
    public int FilmId { get; set; }
    public int UserId { get; set; }
    public string CollectionName { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}


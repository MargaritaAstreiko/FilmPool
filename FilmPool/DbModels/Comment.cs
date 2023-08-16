namespace FilmPool.DbModels
{
  public class Comments
  {
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    public Film Film { get; set; }
    public User User { get; set; }

  }
}

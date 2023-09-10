namespace FilmPool.DbModels
{
    public class Collections
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CollectionName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsPublic { get; set; }
        public User User { get; set; }
        public ICollection<FilmsInCollections> Collctions { get; set; }

    }
}

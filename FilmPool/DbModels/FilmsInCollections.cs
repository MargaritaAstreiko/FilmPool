namespace FilmPool.DbModels
{
    public class FilmsInCollections
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int FilmId { get; set; }
        public DateTime AddedDate { get; set; }
        public Film Film{ get; set; }
        public Collections Collection { get; set; }
    }
}

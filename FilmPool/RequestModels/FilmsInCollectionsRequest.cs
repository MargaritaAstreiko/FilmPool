namespace FilmPool.RequestModels
{
    public class FilmsInCollectionsRequest
    {
        public int CollectionId { get; set; }
        public int FilmId { get; set; }
        public DateTime AddedDate { get; set; }
    }
}

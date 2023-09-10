namespace FilmPool.ResponseModels
{
    public class CollectionsResponseModel
    {
        public int Id { get; set; }
        public string CollectionName { get; set; }
        public IEnumerable<string> filmNames { get; set; }   
    }
}

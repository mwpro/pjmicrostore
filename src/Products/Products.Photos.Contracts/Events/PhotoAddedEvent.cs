namespace Products.Photos.Contracts.Events
{
    public class PhotoAddedEvent
    {
        public int ProductId { get; set; }
        public int PhotoId { get; set; }

        public string OriginalUrl { get; set; }
    }
}
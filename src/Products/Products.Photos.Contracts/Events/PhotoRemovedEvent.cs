namespace Products.Photos.Contracts.Events
{
    public class PhotoRemovedEvent
    {
        public int ProductId { get; set; }
        public int PhotoId { get; set; }

        public string OriginalUrl { get; set; }
    }
}

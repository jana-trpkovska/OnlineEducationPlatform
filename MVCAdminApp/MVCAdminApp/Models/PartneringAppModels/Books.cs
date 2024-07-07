using DocumentFormat.OpenXml.Bibliography;

namespace MVCAdminApp.Models.PartneringAppModels
{
    public class Books
    {
        public Guid Id { get; set; }
        public string? BookImage { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }
        public ICollection<BooksInShoppingCart>? BooksInShoppingCarts { get; set; }

    }
}

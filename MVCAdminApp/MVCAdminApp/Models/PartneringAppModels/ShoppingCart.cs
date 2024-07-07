namespace MVCAdminApp.Models.PartneringAppModels
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public string? OwnerId { get; set; }
        public BookStoreUsers? Owner { get; set; }
        public ICollection<BooksInShoppingCart>? BooksInShoppingCarts { get; set; }

    }
}

namespace MVCAdminApp.Models.PartneringAppModels
{
    public class BooksInShoppingCart
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public Books? Book { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }

    }
}

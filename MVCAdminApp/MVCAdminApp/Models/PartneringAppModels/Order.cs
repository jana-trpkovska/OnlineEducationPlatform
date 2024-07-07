namespace MVCAdminApp.Models.PartneringAppModels
{
    public class Order
    {
        public Guid Id { get; set; }
        public string userId { get; set; }
        public BookStoreUsers User { get; set; }
        public ICollection<BookInOrder> bookInOrders { get; set; }

    }
}

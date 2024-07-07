using DocumentFormat.OpenXml.Drawing.Charts;
using static System.Reflection.Metadata.BlobBuilder;

namespace MVCAdminApp.Models.PartneringAppModels
{
    public class BookInOrder
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Books Book { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }

    }
}

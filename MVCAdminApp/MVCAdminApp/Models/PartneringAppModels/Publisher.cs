namespace MVCAdminApp.Models.PartneringAppModels
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Books>? Books { get; set; }

    }
}

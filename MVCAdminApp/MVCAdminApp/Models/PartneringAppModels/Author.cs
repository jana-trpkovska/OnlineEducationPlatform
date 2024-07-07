namespace MVCAdminApp.Models.PartneringAppModels
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Books>? WrittenBooks { get; set; }

    }
}

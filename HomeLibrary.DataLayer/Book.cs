namespace HomeLibrary.DataLayer
{
    public class BookCopy
    {
        public int BookCopyId { get; set; }
        public Book Book { get; set; }
        public Localisation Localisation { get; set; }
        public User Holder { get; set; }

    }

    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string FacebookId { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public string ISBN { get; set; }
        public Condition Condition { get; set; }
        public int PublishedYear { get; set; }
        public Publisher Publisher { get; set; }
    }

    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Fullname
        {
            get { return $"{Name} {Fullname}"; }
        }
    }

    public class Localisation
    {
        public int LocalisationId { get; set; }
        public string Name { get; set; }
    }

    public class Condition
    {
        public int ConditionId { get; set; }
        public string Name { get; set; }
    }

    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
    }

}
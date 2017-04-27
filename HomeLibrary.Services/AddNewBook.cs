namespace HomeLibrary.Services
{
    public class AddNewBook : IQueryHandler<AddNewBookQuery, bool>
    {
        public bool Handle(AddNewBookQuery query)
        {
            return false;
        }
    }
}
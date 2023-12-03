namespace OOP_Project
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string Nickname { get; set; }
      

        User(string firstName, string lastName, string password, string nickname)
        {
            throw new NotImplementedException();
        }       
    }
}

using System;

namespace OOP_Project
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }

        public User(string firstName, string lastName, string password, string nickname)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Nickname = nickname;
        }
    }
}

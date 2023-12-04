namespace OOP_Project
{
    public class UserJSONDao : Dao<User>, IUserDao<User>
    {
        public override void Create(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<User> Find(Predicate<User> filter = null)
        {
            throw new NotImplementedException();
        }             

        public User FindByNickname(string nickname)
        {
            throw new NotImplementedException();
        }

        public override User FindOne(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(User entity)
        {
            throw new NotImplementedException();
        }       
    }
}

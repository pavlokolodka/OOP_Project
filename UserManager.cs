namespace OOP_Project
{
    public class UserManager
    {
        private IUserDao<User> UserDao;

        public void createUser(User user)
        {
            throw new NotImplementedException();
        }
        public User findUser(string nickname)
        {
            throw new NotImplementedException();
        }

        public void updateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public UserManager(IUserDao<User> userDao)
        {
            throw new NotImplementedException();
        }
    }
}

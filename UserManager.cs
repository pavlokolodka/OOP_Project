namespace OOP_Project
{
    public class UserManager
    {
        private IUserDao<User> UserDao;

        public User createUser(User user)
        {
            var createdUser = UserDao.Create(user);
            return createdUser;
        }
        public User findUser(string nickname)
        {
            return UserDao.FindByNickname(nickname);
        }

        public void updateUser(User user)
        {
            UserDao.Update(user);
        }

        public void deleteUser(int userId)
        {
           UserDao.Delete(userId);
        }

        public UserManager(IUserDao<User> userDao)
        {
            this.UserDao = userDao;
        }
    }
}

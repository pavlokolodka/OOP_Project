namespace OOP_Project
{
    public class UserManager
    {
        public Logger? userLogger;
        private IUserDao<User> UserDao;

        public User createUser(User user)
        {
            var createdUser = UserDao.Create(user);
            userLogger?.Invoke($"User {createdUser.Id} has been created");
            return createdUser;
        }
        public User findUser(string nickname)
        {
            return UserDao.FindByNickname(nickname);
        }

        public void updateUser(User user)
        {
            UserDao.Update(user);
            userLogger?.Invoke($"User {user.Id} has been updated");
        }

        public void deleteUser(int userId)
        {
           UserDao.Delete(userId);
            userLogger?.Invoke($"User {userId} has been deleted");
        }

        public UserManager(IUserDao<User> userDao)
        {
            this.UserDao = userDao;
           
        }
    }
}

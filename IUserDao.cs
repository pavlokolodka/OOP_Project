namespace OOP_Project
{
    public interface IUserDao<T> : IDao<T> 
    {
        T FindByNickname(string nickname);
    }
}

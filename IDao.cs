namespace OOP_Project
{
    public interface IDao<T>
    {
        void Update(T entity);
        void Delete(int id);
        void Create(T entity);
        IEnumerable<T> Find(Func<T, bool> filter = null);
        T FindOne(int id);
    }
}

namespace OOP_Project
{
    public interface IDao<T>
    {
        void Update(T entity);
        void Delete(int id);
        T Create(T entity);
        IEnumerable<T> Find(Predicate<T> filter = null);
        T FindOne(int id);
    }
}

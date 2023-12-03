namespace OOP_Project
{
    public abstract class Dao<T> : IDao<T> where T : IEntity
    {
        protected T GenerateUpdatedAt(T entity)
        {
            throw new NotImplementedException();
        }

        protected T GenerateId(T entity)
        {
            throw new NotImplementedException();
        }

        abstract public void Update(T entity);

        abstract public void Delete(int id);

        abstract public void Create(T entity);

        abstract public IEnumerable<T> Find(Func<T, bool> filter = null);

        abstract public T FindOne(int id);       
    }
}

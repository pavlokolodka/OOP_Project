namespace OOP_Project
{
    public abstract class Dao<T> : IDao<T> where T : IEntity
    {
        abstract protected List<IEntity> Entities { get; set; }

        protected T GenerateUpdatedAt(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            return entity;
        }

        protected int GenerateId()
        {
            return Entities.Count + 1;
        }

        abstract public void Update(T entity);

        abstract public void Delete(int id);

        abstract public T Create(T entity);

        abstract public IEnumerable<T> Find(Predicate<T> filter = null);

        abstract public T FindOne(int id);       
    }
}

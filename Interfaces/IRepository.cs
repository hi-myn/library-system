namespace library_system.Interfaces;
public interface IRepository<T>
{
    void Add(T entity);
    T? GetById(string id);
    IEnumerable<T> GetAll();
    void Remove(string id);
    void Update(T entity);
}


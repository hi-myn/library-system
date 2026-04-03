using library_system.Interfaces;

namespace library_system.Repositories;
 
 public class InMemoryRepository<T> : IRepository<T>
{
    //The "database"
    private readonly Dictionary<string, T> _store = new();

    //A function that returns the ID for each type
    private readonly Func<T, string> _idSelector;

    public InMemoryRepository(Func<T, string> idSelector)
    {
        _idSelector = idSelector;
    }
    public void Add(T entity)
    {
        var id = _idSelector(entity); //Get the object ID
        _store[id] = entity; //Save to dictionary
    }

    public T? GetById(string id)
    {
        _store.TryGetValue(id, out var entity); //Try searching by ID
        return entity; //Returns null if not found
    }
    public IEnumerable<T> GetAll()
    {
        return _store.Values;
    }

    //Overwrites the existing value
    public void Update(T entity)
    {
        var id = _idSelector(entity);
        _store[id] = entity;
    }

    // Removes by ID
    public void Remove(string id)
    {
        _store.Remove(id);
    }
}

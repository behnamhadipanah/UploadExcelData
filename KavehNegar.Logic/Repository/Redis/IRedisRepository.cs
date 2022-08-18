namespace UploadExcelData.Logic.Repository.Redis;

public interface IRedisRepository<T> : IBaseRepository
{
    IList<string> GetAllKeys();
        
    IList<string> SearchKeys(string pattern);
    IList<T> GetAll(string key);
        
    IList<T> GetAll();
    List<T> GetValues(List<string> keys);
    T GetByIndex(string key, int index);
    IDisposable LockEntity(long id);
    bool Remove(string key);
    bool RemoveAll(List<string> keys);
    bool RemoveByIndex(string key, int index);
    bool RemoveByValue(string key, T value);
    void Add(string key, T entity);
    bool UpdateItemAt(string key, int index, T entity);
}
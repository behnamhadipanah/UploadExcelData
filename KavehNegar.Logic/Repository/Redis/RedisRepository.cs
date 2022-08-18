using UploadExcelData.Logic.Context;
using UploadExcelData.Logic.Model;

namespace UploadExcelData.Logic.Repository.Redis;

public class RedisRepository<T> : BaseRepository, IRedisRepository<T> where T:RedisDB
{
    public RedisRepository(IRedisContext redis) : base(redis)
    {
    }

    #region Body

    public IList<string> GetAllKeys()
    {
        return Redis.GetAllKeys();
    }

      
    public void Add(string key, T entity)
    {
        Redis.AddItemToList(GetFullKeyName(key), ToJsonString(entity));

    }
    //public void Add(string key, string entity)
    //{
    //    _redis.AddItemToList(GetFullKeyName(key), ToJsonString(entity));

    //}

    public IList<T> GetAll(string key)
    {
        var strItems = Redis.GetAllItemsFromList(key);
        return strItems.Select(Deserialize<T>).ToList();
    }

    //public string GetAll(string key)
    //{
    //    return _redis.GetAllItemsFromList(key);
            
    //}

    public IList<T> GetAll()
    {
        return Redis.As<T>().GetAll();
    }

    public IList<string> SearchKeys(string pattern)
    {
        return Redis.SearchKeys($"{pattern}*");
    }

    public List<T> GetValues(List<string> keys)
    {
        return Redis.GetValues<T>(keys);
    }

    public T GetByIndex(string key, int index)
    {
        var item = Redis.GetItemFromList(GetFullKeyName(key), index);
        return Deserialize<T>(item);
    }

    public IDisposable LockEntity(long id)
    {
        throw new NotImplementedException();
    }

    public bool Remove(string key)
    {
        Redis.RemoveAllFromList(GetFullKeyName(key));
        return true;
    }

    public bool RemoveAll(List<string> keys)
    {
        Redis.RemoveAll(keys);
        return true;
    }

    public bool RemoveByIndex(string key, int index)
    {
        throw new NotImplementedException();
    }

    public bool RemoveByValue(string key, T value)
    {
        return Redis.RemoveItemFromList(GetFullKeyName(key), ToJsonString(value)) > 0;
    }

    public bool UpdateItemAt(string key, int index, T entity)
    {
        throw new NotImplementedException();
    }

    protected virtual string GetFullKeyName(string key)
    {
        return $"{GetEntityName<T>()}:{key}";
    }

    #endregion





}
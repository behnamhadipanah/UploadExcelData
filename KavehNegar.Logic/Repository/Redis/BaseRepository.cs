using System.ComponentModel;
using Newtonsoft.Json;
using ServiceStack.Redis;
using UploadExcelData.Logic.Context;

namespace UploadExcelData.Logic.Repository.Redis;

public class BaseRepository : IBaseRepository
{
    #region Contractor 
    protected IRedisClient Redis => _rdx.Context;
    private readonly IRedisContext _rdx;
    public BaseRepository(IRedisContext redis)
    {
        _rdx = redis;
    }

    #endregion

    #region Body

    protected string ToJsonString(object item)
    {
        var output = JsonConvert.SerializeObject(item);
        return output;
    }

    protected T Deserialize<T>(string itemStr)
    {
        var output = JsonConvert.DeserializeObject<T>(itemStr);
        return output;
    }

    protected string GetEntityName<T>()
    {
        return TypeDescriptor.GetClassName(typeof(T)).Split('.').Last();
    }

    public void Dispose()
    {
        //_rdx.Dispose();
        //_rdx = null;
        GC.SuppressFinalize(this);
    }

    #endregion
        
}
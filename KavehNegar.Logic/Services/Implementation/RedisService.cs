using UploadExcelData.Logic.Context;
using UploadExcelData.Logic.Contracts;
using UploadExcelData.Logic.Model;
using UploadExcelData.Logic.Repository.Redis;

namespace UploadExcelData.Logic.Services.Implementation;

public class RedisService : IWriteStructure<List<RedisDB>>, IReadStructure<List<RedisDB>>
{

    #region Property
    private readonly IServiceProvider _serviceProvider;
    #endregion
    #region Construnctor

    public RedisService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    #endregion


    #region Override Methods
    public List<RedisDB> Read()
    {
        var list = new List<RedisDB>();

        var redisContext = (IRedisContext)_serviceProvider.GetService(typeof(IRedisContext));
        var repo = new RedisRepository<RedisDB>(redisContext);
        IList<string> keys = repo.GetAllKeys();

        foreach (var key in keys)
        {
            IList<RedisDB> data = repo.GetAll(key);

            foreach (var item in data)
            {
                list.Add(item);
            }
        }

        return list;
    }

    public bool Write(List<RedisDB> entity)
    {
        var redisContext = (IRedisContext)_serviceProvider.GetService(typeof(IRedisContext));
        var repo = new RedisRepository<RedisDB>(redisContext);
        var allKeys = repo.GetAllKeys();
        foreach (var item in entity)
        {
            if (!allKeys.Contains(item.Key))
                repo.Add(item.Key, item);
        }

        //repo.Add(entity);
        return true;
    }

    #endregion

}
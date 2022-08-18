using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using UploadExcelData.Logic.Model;

namespace UploadExcelData.Logic.Context;

public class RedisContext : IRedisContext
{
    #region Property
    public IRedisClient Context { get; set; }

    private readonly RedisDBConfigModel _radisDBConfig;
    #endregion

    #region Body

    public RedisContext(IOptions<RedisDBConfigs> options)
    {
        _radisDBConfig = options.Value.Configs.FirstOrDefault(c => c.Name == "redisDb");

        Context = new RedisClient(_radisDBConfig.Host, _radisDBConfig.Port);
        try
        {
            Context.Db = _radisDBConfig.DBNumber;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    #endregion

    #region dispose
        
    public void Dispose()
    {
        if (Context != null)
            Context.Dispose();
        Context = null;
    }

    #endregion

}
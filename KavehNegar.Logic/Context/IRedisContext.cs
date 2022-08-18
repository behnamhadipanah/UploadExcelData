using ServiceStack.Redis;

namespace UploadExcelData.Logic.Context;

public interface IRedisContext : System.IDisposable
{
    IRedisClient Context { get; set; }
}
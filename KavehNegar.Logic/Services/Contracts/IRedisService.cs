using UploadExcelData.Logic.Contracts;

namespace UploadExcelData.Logic.Services.Contracts;

public interface IRedisService<T>:IReadStructure<List<T>>,IWriteStructure<T>
{
}
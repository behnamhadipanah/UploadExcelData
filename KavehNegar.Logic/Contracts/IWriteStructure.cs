namespace UploadExcelData.Logic.Contracts;

public interface IWriteStructure<T>
{
    bool Write(T entity);
}
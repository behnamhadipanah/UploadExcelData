using UploadExcelData.Logic.SharedKernel;

namespace UploadExcelData.Logic.Model.Entity;

public class ExcelFile : AggergrateRoot
{
    public string Key { get; set; }
    public string Person { get; set; }
    public decimal Sales { get; set; }
    public DateTime Date { get; set; }
}
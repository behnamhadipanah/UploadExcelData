namespace UploadExcelData.Logic.Dtos;

public class ExcelDto
{
    public string Key { get; set; }
    public ExcelValueDto Value { get; set; }
}
public class ExcelValueDto
{
    public string Person { get; set; }
    public string Sales { get; set; }
    public DateTime Date { get; set; }
}
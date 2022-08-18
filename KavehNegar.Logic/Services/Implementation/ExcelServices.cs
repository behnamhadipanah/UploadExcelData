using ExcelDataReader;
using UploadExcelData.Logic.Contracts;
using UploadExcelData.Logic.Dtos;
using UploadExcelData.Logic.Model;

namespace UploadExcelData.Logic.Services.Implementation;

public class ExcelServices : IReadStructure<List<RedisDB>>
{
    private string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "Files", "ExcelFile.xlsx");
    public List<RedisDB> Read()
    {
        var excelDto = new List<RedisDB>();
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using (var stream = File.Open(pathFile, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    int rowNumber = 0;
                    List<string> headerlist = new List<string>();

                    while (reader.Read()) //Each ROW
                    {
                        var dic = new Dictionary<string, string>();

                        rowNumber++;
                        for (int column = 0; column < reader.FieldCount; column++)
                        {
                            if (rowNumber==1)
                            {
                                headerlist.Add(reader.GetValue(column).ToString());
                            }
                            else
                            {
                                dic.Add(headerlist[column].Trim(), reader.GetValue(column).ToString().Trim());
                            }
                        }
                        for (int i = 0; i < dic.Count; i++)
                        {
                            excelDto.Add(new RedisDB()
                            {
                                Key = dic["Persons"]+"_"+Convert.ToDateTime(dic["Date"]).ToString("yyyyMMdd"),
                                Value = new ExcelValueDto()
                                {
                                    Person = dic["Persons"],
                                    Date = Convert.ToDateTime(dic["Date"]),
                                    Sales = dic["Sales"]
                                }
                            });
                        }
                    }
                } while (reader.NextResult()); //Move to NEXT SHEET
            }
        }

        return excelDto;
    }
}
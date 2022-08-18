using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using UploadExcelData.Logic.Contracts;
using UploadExcelData.Logic.Model;
using UploadExcelData.Logic.Model.Entity;

namespace UploadExcelData.Logic.Services.Implementation;

public class SqlService : IWriteStructure<List<RedisDB>>
{
    #region Property

    private readonly IConfigurationRoot _configurationRoot;
    #endregion

    #region Construnctor

    public SqlService(IConfigurationRoot configurationRoot)
    {
        _configurationRoot = configurationRoot;
    }

    #endregion

    public bool Write(List<RedisDB> entity)
    {
        var mapData = Map(entity);
        using (var connection = new SqlConnection(_configurationRoot.GetConnectionString("sqlConnection")))
        {

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
            {

                try
                {
                    foreach (var item in mapData)
                    {
                        connection.Execute("[Kn].[Insert_Into_ExcelFile]", new
                        {
                            Key=item.Key,
                            Person=item.Person,
                            Date=item.Date.Date,
                            Sales=item.Sales
                        },commandType:CommandType.StoredProcedure,transaction:transaction);
                    }
             
                }

                catch (Exception e)
                {

                    Console.WriteLine(e.Message);


                    transaction.Rollback();
                    connection.Close();
                }
            }

            transaction.Commit();
            connection.Close();

        }

        return true;
    }


    private List<ExcelFile> Map(List<RedisDB> redisDbs)
    {
        List<ExcelFile> result = new List<ExcelFile>();

        foreach (var item in redisDbs)
        {

            var data = ((JObject)item.Value).Root;
            result.Add(new ExcelFile()
            {
                Key = item.Key,
                Person = data["Person"].ToString(),
                Date = Convert.ToDateTime(data["Date"]),
                Sales = Convert.ToDecimal(data["Sales"])
            });
                

        }


        return result;
    }

      
}
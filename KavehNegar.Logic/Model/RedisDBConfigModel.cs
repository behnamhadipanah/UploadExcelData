namespace UploadExcelData.Logic.Model;

public class RedisDBConfigModel
{
    public string Name { get; set; }
    public int DBNumber { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
}
public class RedisDBConfigs
{
    public List<RedisDBConfigModel?> Configs { get; set; }
}
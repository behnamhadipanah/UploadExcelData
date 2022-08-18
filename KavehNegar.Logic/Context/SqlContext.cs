using Microsoft.EntityFrameworkCore;
using UploadExcelData.Logic.Model.Entity;
using UploadExcelData.Logic.SharedKernel;

namespace UploadExcelData.Logic.Context;

public class SqlContext : DbContext, ISqlContext
{

    #region Constructor

    public SqlContext(DbContextOptions<SqlContext> options) : base(options)
    {

    }


    #endregion

    #region DbSet
    public DbSet<ExcelFile> ExcelFiles { get; set; }

    #endregion

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    public DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
    {
        return base.Set<TEntity>();
    }
}
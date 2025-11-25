using Core;

namespace Service.Interfaces;

public interface ISqlTAbleGeneratorService
{
    string GenerateDropTableScript(string schema, string tableName);
    string GenerateRenameTableScript(string schema, string oldTableName, string newTableName);

    string GenerateCreateTableScript(TableCreateViewModel model);

    string GenerateTableStorageUpdateScript(TableStorageUpdateViewModel model);

     string GenerateCreateDatabaseScript(string databaseName, string recoveryModel, string fileGroupName, string dataFilePath, string logFilePath, bool isDeleteIfExists, string dataFileSize, string logFileSize, string maxDataFileSize, string maxLogFileSize, string autoGrowthIncrement);


}

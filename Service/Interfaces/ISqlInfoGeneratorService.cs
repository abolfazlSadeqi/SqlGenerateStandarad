namespace Service.Interfaces;

public interface ISqlInfoGeneratorService
{

    public string GenerateTableScript(string schemaName, string tableName);

    public string GenerateViewScript(string schemaName, string viewName);

    public string GenerateIndexScript(string tableName, string indexName);


    // اسکریپت جستجو ستون‌ها
    public string GenerateColumnScript(string tableName, string columnName);
    public string GenerateDatabaseInfoScript(string databaseName);
    public string GenerateProcedureScript(string schemaName, string procedureName);
    public string GeneratePrimaryKeyScript(string tableName, string primaryKeyName);

    public string GenerateForeignKeyScript(string tableName, string foreignKeyName);

    public string GenerateSchemaScript(string schemaName);

    public string GenerateFileGroupScript(string fileGroupName);
}

using Service.Interfaces;

namespace Service.Services;

public class SqlInfoGeneratorService : ISqlInfoGeneratorService
{
    public string GenerateTableScript(string schemaName, string tableName)
    {
        return $@"
SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = '{schemaName}' AND TABLE_NAME = '{tableName}'
";
    }

    public string GenerateViewScript(string schemaName, string viewName)
    {
        schemaName = schemaName.Replace("'", "''");
        viewName = viewName.Replace("'", "''");

        return $@"
IF EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.VIEWS
    WHERE TABLE_SCHEMA = '{schemaName}' 
      AND TABLE_NAME = '{viewName}'
)
BEGIN
    PRINT 'View exists.'
END
ELSE
BEGIN
    PRINT 'View does not exist.'
END
";

    }

    public string GenerateIndexScript(string tableName, string indexName)
    {
        return $@"
SELECT *
FROM sys.indexes
WHERE OBJECT_NAME(OBJECT_ID) = '{tableName}' AND name = '{indexName}'
";
    }


    // اسکریپت جستجو ستون‌ها
    public string GenerateColumnScript(string tableName, string columnName)
    {
        return $@"
SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = '{tableName}' AND COLUMN_NAME = '{columnName}'
";
    }
    public string GenerateDatabaseInfoScript(string databaseName)
    {
        return $@"
SELECT 
name, 
database_id, 
state_desc 
FROM sys.databases
WHERE name = '{databaseName}'
";
    }
    public string GenerateProcedureScript(string schemaName, string procedureName)
    {
        return $@"
SELECT * FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_SCHEMA = '{schemaName}' AND ROUTINE_NAME = '{procedureName}'
";
    }
    public string GeneratePrimaryKeyScript(string tableName, string primaryKeyName)
    {
        return $@"
SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE TABLE_NAME = '{tableName}' AND CONSTRAINT_NAME = '{primaryKeyName}' AND CONSTRAINT_TYPE = 'PRIMARY KEY'
";
    }

    public string GenerateForeignKeyScript(string tableName, string foreignKeyName)
    {
        return $@"
SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE TABLE_NAME = '{tableName}' AND CONSTRAINT_NAME = '{foreignKeyName}'
";
    }

    public string GenerateSchemaScript(string schemaName)
    {
        return $@"
SELECT * FROM INFORMATION_SCHEMA.SCHEMATA
WHERE SCHEMA_NAME = '{schemaName}'
";
    }

    public string GenerateFileGroupScript(string fileGroupName)
    {
        return $@"
SELECT * FROM sys.filegroups
WHERE name = '{fileGroupName}'
";
    }
}
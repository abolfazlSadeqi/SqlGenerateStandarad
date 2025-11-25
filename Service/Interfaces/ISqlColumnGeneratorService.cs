namespace Service.Interfaces;

public interface ISqlColumnGeneratorService
{
    string GenerateAddColumnScript(string schema, string tableName, string columnName, string dataType, bool isNullable, string defaultValue);
    string GenerateAlterColumnScript(string schema, string tableName, string columnName, string dataType, bool isNullable);
    string GenerateDropColumnScript(string schema, string tableName, string columnName);
    string GenerateRenameColumnScript(string schema, string tableName, string oldColumnName, string newColumnName);
   
}

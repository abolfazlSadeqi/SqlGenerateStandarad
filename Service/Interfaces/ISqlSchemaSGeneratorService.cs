using Core;

namespace Service.Interfaces;

public interface ISqlSchemaSGeneratorService
{
    public string GenerateCreateSchemaScript(CreateSchemaViewModel model);

    string GenerateDropSchemaScript(DropSchemaViewModel model);

    string GenerateRenameSchemaScript(RenameSchemaViewModel model);
}

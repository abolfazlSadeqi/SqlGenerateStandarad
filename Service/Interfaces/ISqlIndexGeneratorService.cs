using Core;

namespace Service.Interfaces;

public interface ISqlIndexGeneratorService
{

    string GenerateCreateIndexScript(IndexManageViewModel model);

    string GenerateDropIndexScript(IndexManageViewModel model);
}

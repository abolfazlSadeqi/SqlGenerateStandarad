using Core;

namespace Service.Interfaces;

public interface ISqlFunctionGeneratorService
{
    string GenerateCreateFunctionScript(CreateFunctionViewModel model);

    string GenerateDropFunctionScript(DropFunctionViewModel model);
}

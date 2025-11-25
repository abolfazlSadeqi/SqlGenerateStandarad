using Core;

namespace Service.Interfaces;

public interface ISqlFileGroupGeneratorService
{
    string GenerateAddFileGroupScript(FileGroupViewModel model);
    string GenerateDropFileGroupScript(FileGroupViewModel model);


    string GeneratePartitionFunctionAndScheme(PartitionCreateViewModel model);
}

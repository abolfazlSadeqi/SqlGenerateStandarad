using Core;

namespace Service.Interfaces;

public interface ISqlSequenceGeneratorService
{

    string GenerateDropSequenceScript(DropSequenceViewModel model);
    public string GenerateAdvancedCreateSequenceScript(AdvancedSequenceManageViewModel model);
 

}

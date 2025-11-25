using Core;

namespace Service.Interfaces;

public interface ISqlViewGeneratorService
{
    string GenerateCreateViewScript(ViewManageViewModel model);

    string GenerateDropViewScript(DropViewViewModel model);
    string GenerateCreateProcedureScript(ProcedureManageViewModel model);


    string GenerateDropProcedureScript(DropProcedureViewModel model);

}

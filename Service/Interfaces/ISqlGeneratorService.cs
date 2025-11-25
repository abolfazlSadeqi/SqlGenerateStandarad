using Core;
using System.Text;

namespace Service.Interfaces;
public interface ISqlGeneratorService 
{
     string GenerateCreateTempDbScript(TempDbSettings settings);
    
}

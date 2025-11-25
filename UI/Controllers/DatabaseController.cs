using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly ISqlTAbleGeneratorService _sqlGen;

        public DatabaseController(ISqlTAbleGeneratorService sqlGen)
        {
            _sqlGen = sqlGen;
        }

        // GET: Database/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new DatabaseCreateViewModel());
        }

        // POST: Database/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DatabaseCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Generate SQL script for creating the database
            var createDbScript = _sqlGen.GenerateCreateDatabaseScript(
                model.DatabaseName,
                model.RecoveryModel,
                model.FileGroupName,
                model.DataFilePath,
                model.LogFilePath,
                model.IsDeleteIfExists,
                model.DataFileSize,
                model.LogFileSize,
                model.MaxDataFileSize,
                model.MaxLogFileSize,
                model.AutoGrowthIncrement
            );

            model.GeneratedScript = createDbScript;

            return View(model);
        }
    }
}

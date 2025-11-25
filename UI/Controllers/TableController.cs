using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class TableController : Controller
{
    private readonly ISqlTAbleGeneratorService _sqlService;

    public TableController(ISqlTAbleGeneratorService sqlService)
    {
        _sqlService = sqlService;
    }

    // -----------------------------------
    // Drop Table
    // -----------------------------------
    [HttpGet]
    public IActionResult DropTable()
    {
        return View(new TableDropViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DropTable(TableDropViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateDropTableScript(
                model.Schema,
                model.TableName
            );
        }
        return View(model);
    }

    // -----------------------------------
    // Rename Table
    // -----------------------------------
    [HttpGet]
    public IActionResult RenameTable()
    {
        return View(new TableRenameViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RenameTable(TableRenameViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateRenameTableScript(
                model.Schema,
                model.OldTableName,
                model.NewTableName
            );
        }
        return View(model);
    }
    [HttpGet]
    public IActionResult CreateTable()
    {
        var model = new TableCreateViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateTable(TableCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateCreateTableScript(model);
        }
        return View(model);
    }
    [HttpGet]
    public IActionResult UpdateTableStorage()
    {
        return View(new TableStorageUpdateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateTableStorage(TableStorageUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateTableStorageUpdateScript(model);
        }
        return View(model);
    }

}


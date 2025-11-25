using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class ColumnController : Controller
{
    private readonly ISqlColumnGeneratorService _sqlGen;

    public ColumnController(ISqlColumnGeneratorService sqlGen)
    {
        _sqlGen = sqlGen;
    }

    // GET: Build/CreateColumn
    [HttpGet]
    public IActionResult CreateColumn()
    {
        var model = new ColumnCreateViewModel
        {
            Schema = "dbo",
            IsNullable = true
        };
        return View(model);
    }

    // POST: Build/CreateColumn
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateColumn(ColumnCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Basic sanitization for DataType: ensure it's not empty and trim
        model.DataType = model.DataType?.Trim();

        // Generate script
        var script = _sqlGen.GenerateAddColumnScript(model.Schema, model.TableName, model.ColumnName, model.DataType, model.IsNullable, model.DefaultValue);
        model.GeneratedScript = script;

        // Return same view with script shown
        return View(model);
    }


    // GET
    [HttpGet]
    public IActionResult AlterColumn()
    {
        return View(new ColumnAlterViewModel());
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AlterColumn(ColumnAlterViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GenerateAlterColumnScript(
                model.Schema,
                model.TableName,
                model.ColumnName,
                model.DataType,
                model.IsNullable
            );
        }
        return View(model);
    }
    // GET
    [HttpGet]
    public IActionResult DropColumn()
    {
        return View(new ColumnDropViewModel());
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DropColumn(ColumnDropViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GenerateDropColumnScript(
                model.Schema,
                model.TableName,
                model.ColumnName
            );
        }

        return View(model);
    }
    // GET
    [HttpGet]
    public IActionResult RenameColumn()
    {
        return View(new ColumnRenameViewModel());
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RenameColumn(ColumnRenameViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GenerateRenameColumnScript(
                model.Schema,
                model.TableName,
                model.OldColumnName,
                model.NewColumnName
            );
        }

        return View(model);
    }

}


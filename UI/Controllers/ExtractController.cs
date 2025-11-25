using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class ExtractController : Controller
{


    private readonly ISqlInfoGeneratorService _sqlGen;

    public ExtractController(ISqlInfoGeneratorService sqlGen)
    {
        _sqlGen = sqlGen;
    }

    // GET: Extract/Tables
    [HttpGet]
    public IActionResult Tables()
    {
        return View(new TableViewModel());
    }

    // POST: Extract/Tables
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Tables(TableViewModel model)
    {
        if (ModelState.IsValid)
        {
            // اسکریپت جستجو برای جدول
            model.GeneratedScript = _sqlGen.GenerateTableScript(model.SchemaName, model.TableName);
            return View(model);
        }

        return View(model);
    }

    // اسکریپت جستجو جدول


    // GET: Extract/Views
    [HttpGet]
    public IActionResult Views()
    {
        return View(new ViewViewModel());
    }

    // POST: Extract/Views
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Views(ViewViewModel model)
    {
        if (ModelState.IsValid)
        {
            // اسکریپت جستجو برای ویو
            model.GeneratedScript = _sqlGen.GenerateViewScript(model.SchemaName, model.ViewName);
            return View(model);
        }

        return View(model);
    }

    // اسکریپت جستجو ویو

    // GET: Extract/Indexes
    [HttpGet]
    public IActionResult Indexes()
    {
        return View(new IndexViewModel());
    }

    // POST: Extract/Indexes
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Indexes(IndexViewModel model)
    {
        if (ModelState.IsValid)
        {
            // اسکریپت جستجو برای ایندکس‌ها
            model.GeneratedScript = _sqlGen.GenerateIndexScript(model.TableName, model.IndexName);
            return View(model);
        }

        return View(model);
    }

    // اسکریپت جستجو ایندکس‌ها


    // GET: Extract/Columns
    [HttpGet]
    public IActionResult Columns()
    {
        return View(new ColumnViewModel());
    }

    // POST: Extract/Columns
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Columns(ColumnViewModel model)
    {
        if (ModelState.IsValid)
        {
            // اسکریپت جستجو برای ستون‌ها
            model.GeneratedScript = _sqlGen.GenerateColumnScript(model.TableName, model.ColumnName);
            return View(model);
        }

        return View(model);
    }


    // --- File Groups ---
    [HttpGet]
    public IActionResult FileGroups()
    {
        return View(new FileGroupInfoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult FileGroups(FileGroupInfoViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GenerateFileGroupScript(model.FileGroupName);
            return View(model);
        }

        return View(model);
    }

   

    // --- Schemas ---
    [HttpGet]
    public IActionResult Schemas()
    {
        return View(new SchemaViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Schemas(SchemaViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GenerateSchemaScript(model.SchemaName);
            return View(model);
        }

        return View(model);
    }

  

    // --- Foreign Keys ---
    [HttpGet]
    public IActionResult ForeignKeys()
    {
        return View(new ForeignKeyViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ForeignKeys(ForeignKeyViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GenerateForeignKeyScript(model.TableName, model.ForeignKeyName);
            return View(model);
        }

        return View(model);
    }

  

    // --- Primary Keys ---
    [HttpGet]
    public IActionResult PrimaryKeys()
    {
        return View(new PrimaryKeyViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PrimaryKeys(PrimaryKeyViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GeneratePrimaryKeyScript(model.TableName, model.PrimaryKeyName);
            return View(model);
        }

        return View(model);
    }

 

    // --- Procedures ---
    [HttpGet]
    public IActionResult Procedures()
    {
        return View(new ProcedureViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Procedures(ProcedureViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GenerateProcedureScript(model.SchemaName, model.ProcedureName);
            return View(model);
        }

        return View(model);
    }

 

    // --- Database Info ---
    [HttpGet]
    public IActionResult DatabaseInfo()
    {
        return View(new DatabaseInfoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DatabaseInfo(DatabaseInfoViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlGen.GenerateDatabaseInfoScript(model.DatabaseName);
            return View(model);
        }

        return View(model);
    }

    


}

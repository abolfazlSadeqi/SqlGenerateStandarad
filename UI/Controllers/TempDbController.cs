using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class TempDbController : Controller
{
    private readonly ISqlGeneratorService _sqlGeneratorService;

    public TempDbController(ISqlGeneratorService sqlGeneratorService)
    {
        _sqlGeneratorService = sqlGeneratorService;
    }

    // GET: TempDb/Create
    [HttpGet]
    public IActionResult CreateTempDb()
    {
        return View(new TempDbSettings());
    }

    // POST: TempDb/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateTempDb(TempDbSettings settings)
    {
        if (ModelState.IsValid)
        {
            // تولید اسکریپت SQL بر اساس تنظیمات ورودی
            var script = _sqlGeneratorService.GenerateCreateTempDbScript(settings);
            settings.GeneratedScript = script;

            return View(settings);
        }

        return View(settings);
    }
}


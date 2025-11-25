using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class FileGroupController : Controller
{
    private readonly ISqlFileGroupGeneratorService _sqlService;

    public FileGroupController(ISqlFileGroupGeneratorService sqlService)
    {
        _sqlService = sqlService;
    }

    [HttpGet]
    public IActionResult ManageFileGroup()
    {
        return View(new FileGroupViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddFileGroup(FileGroupViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateAddFileGroupScript(model);
        }
        return View("ManageFileGroup", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DropFileGroup(FileGroupViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateDropFileGroupScript(model);
        }
        return View("ManageFileGroup", model);
    }
}


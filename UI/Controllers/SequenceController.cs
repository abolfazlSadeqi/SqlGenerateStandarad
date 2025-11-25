using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class SequenceController : Controller
{
    private readonly ISqlSequenceGeneratorService _sqlService;

    public SequenceController(ISqlSequenceGeneratorService sqlService)
    {
        _sqlService = sqlService;
    }
    // -------------------- CREATE --------------------
    [HttpGet]
    public IActionResult CreateSequence()
    {
        return View(new AdvancedSequenceManageViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateSequence(AdvancedSequenceManageViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateAdvancedCreateSequenceScript(model);
        }
        return View(model);
    }


    // -------------------- DROP --------------------
    [HttpGet]
    public IActionResult DropSequence()
    {
        return View(new DropSequenceViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DropSequence(DropSequenceViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateDropSequenceScript(model);
        }
        return View(model);
    }


}


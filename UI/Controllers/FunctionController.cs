using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class FunctionController : Controller
{
    private readonly ISqlFunctionGeneratorService _sqlService;

    public FunctionController(ISqlFunctionGeneratorService sqlService)
    {
        _sqlService = sqlService;
    }

    [HttpGet]
    public IActionResult CreateFunction()
    {
        return View(new CreateFunctionViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateFunction(CreateFunctionViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateCreateFunctionScript(model);
        }
        return View(model);
    }
    [HttpGet]
    public IActionResult DropFunction()
    {
        return View(new DropFunctionViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DropFunction(DropFunctionViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateDropFunctionScript(model);
        }
        return View(model);
    }

}


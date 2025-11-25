using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class ViewController : Controller
{
    private readonly ISqlViewGeneratorService _sqlService;

    public ViewController(ISqlViewGeneratorService sqlService)
    {
        _sqlService = sqlService;
    }

    [HttpGet]
    public IActionResult ManageView()
    {
        return View(new ViewManageViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateView(ViewManageViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateCreateViewScript(model);
        }
        return View("ManageView", model);
    }

    [HttpGet]
    public IActionResult DropView()
    {
        return View(new DropViewViewModel());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DropView(DropViewViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateDropViewScript(model);
        }
        return View("DropView", model);
    }
    

    [HttpGet]
    public IActionResult ManageProcedure()
    {
        return View(new ProcedureManageViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateProcedure(ProcedureManageViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateCreateProcedureScript(model);
        }
        return View("ManageProcedure", model);
    }


    

    [HttpGet]
    public IActionResult DropProcedure()
    {
        return View(new DropProcedureViewModel());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DropProcedure(DropProcedureViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GenerateDropProcedureScript(model);
        }
        return View("DropProcedure", model);
    }

}


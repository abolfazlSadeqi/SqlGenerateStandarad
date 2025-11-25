using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class PartitionController : Controller
{
    private readonly ISqlFileGroupGeneratorService _sqlService;

    public PartitionController(ISqlFileGroupGeneratorService sqlService)
    {
        _sqlService = sqlService;
    }

    [HttpGet]
    public IActionResult CreatePartition()
    {
        var model = new PartitionCreateViewModel();
        model.FileGroups.Add(new PartitionFileGroup()); // پیش‌فرض یک فایل‌گروپ
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreatePartition(PartitionCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _sqlService.GeneratePartitionFunctionAndScheme(model);
        }
        return View(model);
    }
}


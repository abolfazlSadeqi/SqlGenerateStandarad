using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;


public class IndexController : Controller
{
    private readonly ISqlIndexGeneratorService _sqlService;

    public IndexController(ISqlIndexGeneratorService sqlService)
    {
        _sqlService = sqlService;
    }

    // GET: Index/ManageIndex
    [HttpGet]
    public IActionResult ManageIndex()
    {
        var model = new IndexManageViewModel();

        return View(model);
    }

    // POST: Index/ManageIndex
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateIndex(IndexManageViewModel model)
    {
        if (ModelState.IsValid)
        {
            // استفاده از سرویس برای ایجاد SQL مربوط به ایندکس
            model.GeneratedScript = _sqlService.GenerateCreateIndexScript(model);

            // برگرداندن به همان صفحه با اسکریپت تولید شده
            return View("ManageIndex", model);
        }

        // در صورت وجود خطا، به صفحه باز می‌گردد
        return View("ManageIndex", model);
    }

    // POST: Index/DeleteIndex
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DropIndex(IndexManageViewModel model)
    {
        if (ModelState.IsValid)
        {
            // استفاده از سرویس برای ایجاد اسکریپت حذف ایندکس
            model.GeneratedScript = _sqlService.GenerateDropIndexScript(model);

            // برگرداندن به همان صفحه با اسکریپت حذف شده
            return View("ManageIndex", model);
        }

        // در صورت وجود خطا، به صفحه باز می‌گردد
        return View("ManageIndex", model);
    }
}


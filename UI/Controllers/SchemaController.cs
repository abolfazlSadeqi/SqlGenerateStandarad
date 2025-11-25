using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers
{
    public class SchemaController : Controller
    {
        private readonly ISqlSchemaSGeneratorService _sqlService;

        public SchemaController(ISqlSchemaSGeneratorService sqlService)
        {
            _sqlService = sqlService;
        }

        // اکشن برای نمایش ویو مدیریت Schema
        [HttpGet]
        public IActionResult CreateSchema()
        {
            return View(new CreateSchemaViewModel());
        }

        // اکشن برای ایجاد Schema
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSchema(CreateSchemaViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.GeneratedScript = _sqlService.GenerateCreateSchemaScript(model);
            }
            return View(model);
        }

        // اکشن برای نمایش ویو حذف Schema
        [HttpGet]
        public IActionResult DropSchema()
        {
            return View(new DropSchemaViewModel());
        }

        // اکشن برای حذف Schema
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DropSchema(DropSchemaViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.GeneratedScript = _sqlService.GenerateDropSchemaScript(model);
            }
            return View(model);
        }

    }
}

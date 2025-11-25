using Core;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace UI.Controllers;

public class SecurityController : Controller
{
    private readonly ISqlSecurityService _securityService;

    public SecurityController(ISqlSecurityService securityService)
    {
        _securityService = securityService;
    }

    #region Login Management

    // GET: Security/CreateLogin
    [HttpGet]
    public IActionResult CreateLogin()
    {
        return View(new LoginCreateViewModel());
    }

    // POST: Security/CreateLogin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateLogin(LoginCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.CreateLogin(model.LoginName, model.Password, model.IsSqlLogin);
            return View(model);
        }

        return View(model);
    }

    // GET: Security/DeleteLogin
    [HttpGet]
    public IActionResult DeleteLogin()
    {
        return View(new LoginDeleteViewModel());
    }

    // POST: Security/DeleteLogin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteLogin(LoginDeleteViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.DropLogin(model.LoginName);
            return View(model);
        }

        return View(model);
    }

    // GET: Security/ChangeLoginPassword
    [HttpGet]
    public IActionResult ChangeLoginPassword()
    {
        return View(new LoginPasswordViewModel());
    }

    // POST: Security/ChangeLoginPassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangeLoginPassword(LoginPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.ChangeLoginPassword(model.LoginName, model.NewPassword);
            return View(model);
        }

        return View(model);
    }

    #endregion

    #region User Management (Database Level)

    // GET: Security/CreateUser
    [HttpGet]
    public IActionResult CreateUser()
    {
        return View(new UserCreateViewModel());
    }

    // POST: Security/CreateUser
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateUser(UserCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.CreateUser(model.DatabaseName, model.UserName, model.LoginName);
            return View(model);
        }

        return View(model);
    }

    // GET: Security/DeleteUser
    [HttpGet]
    public IActionResult DeleteUser()
    {
        return View(new UserDeleteViewModel());
    }

    // POST: Security/DeleteUser
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteUser(UserDeleteViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.DropUser(model.DatabaseName, model.UserName);
            return View(model);
        }

        return View(model);
    }

    // GET: Security/AddUserToRole
    [HttpGet]
    public IActionResult AddUserToRole()
    {
        return View(new UserRoleAssignViewModel());
    }

    // POST: Security/AddUserToRole
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddUserToRole(UserRoleAssignViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.AddUserToRole(model.DatabaseName, model.UserName, model.RoleName);
            return View(model);
        }

        return View(model);
    }

    #endregion

    #region Permissions Management

    // GET: Security/GrantDatabasePermission
    [HttpGet]
    public IActionResult GrantDatabasePermission()
    {
        return View(new UserPermissionViewModel());
    }

    // POST: Security/GrantDatabasePermission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GrantDatabasePermission(UserPermissionViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.GrantDatabasePermission(model.DatabaseName, model.UserName, model.Permission);
            return View(model);
        }

        return View(model);
    }

    // GET: Security/GrantObjectPermission
    [HttpGet]
    public IActionResult GrantObjectPermission()
    {
        return View(new ObjectPermissionViewModel());
    }

    // POST: Security/GrantObjectPermission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GrantObjectPermission(ObjectPermissionViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.GrantObjectPermission(model.DatabaseName, model.UserName, model.Permission, model.SchemaName, model.ObjectName);
            return View(model);
        }

        return View(model);
    }

    #endregion

    #region Server Role Management

    // GET: Security/AddLoginToServerRole
    [HttpGet]
    public IActionResult AddLoginToServerRole()
    {
        return View(new ServerRoleAssignViewModel());
    }

    // POST: Security/AddLoginToServerRole
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddLoginToServerRole(ServerRoleAssignViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.AddLoginToServerRole(model.LoginName, model.RoleName);
            return View(model);
        }

        return View(model);
    }
    // GET: Security/AddLoginToServerRole
    [HttpGet]
    public IActionResult RevokeObjectPermission()
    {
        return View(new ObjectPermissionViewModel());
    }

    // POST: Security/AddLoginToServerRole
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RevokeObjectPermission(ObjectPermissionViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.GeneratedScript = _securityService.RevokeObjectPermission(model.DatabaseName, model.UserName, model.Permission, model.SchemaName, model.ObjectName);

            return View(model);
        }

        return View(model);
    }
    #endregion
}


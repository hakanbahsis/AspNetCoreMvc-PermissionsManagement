using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PermissionsManagement.Controllers;

public class ProductController : Controller
{
    [Authorize("Permissions.Products.View")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize("Permissions.Products.Create")]
    public IActionResult Create()
    {
        return View();
    }
}

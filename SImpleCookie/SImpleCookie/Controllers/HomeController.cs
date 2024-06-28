using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SImpleCookie.Models;

namespace SImpleCookie.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //read cookie
        string cookieValue = Get("NamePerson");
        ViewBag.CookieValue = cookieValue;
        return View();
    }

    private string Get(string key)
    {
        return Request.Cookies[key] ?? string.Empty;
    }

    public IActionResult Delete()
    {
        //delete cookie
        Remove("NamePerson");
        return RedirectToAction("Index");
    }

    private void Remove(string key)
    {
        Response.Cookies.Delete(key);
    }

    public IActionResult Create()
    {
        //create cookie
        Set("NamePerson", "Bill Gates", 10);
        return RedirectToAction("Index");

    }

    private void Set(string key, string value, int? expireTime)
    {
        CookieOptions option = new CookieOptions();
        if (expireTime.HasValue)
        {
            option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        }
        else
        {
            option.Expires = DateTime.Now.AddMilliseconds(10); 
        }
        
        Response.Cookies.Append(key,value,option);
    }
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
using Microsoft.AspNetCore.Mvc;
using SiteGen.Core.Models;
using System.Diagnostics;

namespace RoboKiwi.com.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
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

    //[Route("/categories/{category}")]
    //public async Task<IActionResult> Category([FromRoute]string? category)
    //{
    //    if (string.IsNullOrWhiteSpace(category)) return NotFound();

    //    var categories = siteMap.Nodes.Where(x => x.Metadata?.Categories != null && x.Metadata.Categories.Count > 0)
    //        .SelectMany(x => x.Metadata.Categories)
    //        .Distinct()
    //        .ToList();

    //    var match = categories.FirstOrDefault(x => string.Equals(x, category, StringComparison.OrdinalIgnoreCase));

    //    if (match == null) return NotFound();

    //    var matches = siteMap.Nodes.Where(x => x.Metadata?.Categories != null && x.Metadata.Categories.Contains(match, StringComparer.OrdinalIgnoreCase)).ToList();

    //    var model = new CategoryPage
    //    { 
    //        Category = match,
    //        Items = matches
    //    };

    //    return View("Category", model);
    //}
}
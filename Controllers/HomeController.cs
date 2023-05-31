using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.IRepository;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPieRepository _pieRepository;
    private readonly IProductRepository _productRepository;

    public HomeController(ILogger<HomeController> logger, IPieRepository pieRepository, IProductRepository productRepository)
    {
        _logger = logger;
        _pieRepository = pieRepository;
        _productRepository = productRepository;
    }

    public IActionResult Index()
    {
        var piesOfTheWeek = _pieRepository.PiesOfTheWeek;
        var products = _productRepository.AllProducts;
        var homeViewModel = new HomeViewModel(piesOfTheWeek, products);
        return View(homeViewModel);
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

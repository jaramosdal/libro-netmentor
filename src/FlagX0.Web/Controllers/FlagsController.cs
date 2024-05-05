using FlagX0.Web.Models;
using FlagX0.Web.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlagX0.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class FlagsController : Controller
{
    private readonly IAddFlagUseCase _addFlagUseCase;

    public FlagsController(IAddFlagUseCase addFlagUseCase)
    {
        _addFlagUseCase = addFlagUseCase;
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new FlagViewModel());
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(FlagViewModel request)
    {
        await _addFlagUseCase.Execute(request.Name, request.IsEnabled);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}

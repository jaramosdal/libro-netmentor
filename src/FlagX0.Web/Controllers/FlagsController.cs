﻿using FlagX0.Web.Models;
using FlagX0.Web.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP;
using System.Security.Claims;

namespace FlagX0.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class FlagsController : Controller
{
    private readonly AddFlagUseCase _addFlagUseCase;
    private readonly GetFlagUseCase _getFlagUseCase;

    public FlagsController(AddFlagUseCase addFlagUseCase, GetFlagUseCase getFlagUseCase)
    {
        _addFlagUseCase = addFlagUseCase;
        _getFlagUseCase = getFlagUseCase;
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new FlagViewModel());
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(FlagViewModel request)
    {
        Result<bool> isCreated = await _addFlagUseCase.Execute(request.Name, request.IsEnabled);

        if (isCreated.Success)
        {
            return RedirectToAction("Index");
        }
        
        return View("Created", new FlagViewModel
        {
            Error = isCreated.Errors.First().Message,
            IsEnabled = request.IsEnabled,
            Name = request.Name
        });
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var listFlags = await _getFlagUseCase.Execute();
        return View(new FlagIndexViewModel()
        {
            Flags = listFlags
        });
    }
}

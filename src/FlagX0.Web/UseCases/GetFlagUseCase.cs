using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using FlagX0.Web.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlagX0.Web.UseCases;

public class GetFlagUseCase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetFlagUseCase(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
    {
        _applicationDbContext = applicationDbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<FlagDto>> Execute()
    {
        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        var response = await _applicationDbContext.Flags
            .Where(a => a.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        return response.Select(a => new FlagDto()
        {
            IsEnabled = a.Value,
            Name = a.Name
        }).ToList();
    }
}

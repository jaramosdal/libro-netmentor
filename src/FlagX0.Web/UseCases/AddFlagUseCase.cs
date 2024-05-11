using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using System.Security.Claims;

namespace FlagX0.Web.UseCases;

public class AddFlagUseCase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddFlagUseCase(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
    {
        _applicationDbContext = applicationDbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> Execute(string name, bool isEnabled)
    {
        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        FlagEntity flagEntity = new FlagEntity 
        { 
            Name = name,
            UserId = userId, 
            Value = isEnabled
        };
        var response = await _applicationDbContext.Flags.AddAsync(flagEntity);
        await _applicationDbContext.SaveChangesAsync();

        return true;
    }
}

using FlagX0.Web.Data;
using FlagX0.Web.DTOs;
using FlagX0.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace FlagX0.Web.UseCases;

public class GetFlagUseCase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IFlagUserDetails _flagUserDetails;

    public GetFlagUseCase(ApplicationDbContext applicationDbContext, IFlagUserDetails flagUserDetails)
    {
        _applicationDbContext = applicationDbContext;
        _flagUserDetails = flagUserDetails;
    }

    public async Task<List<FlagDto>> Execute()
    {
        var response = await _applicationDbContext.Flags
            .Where(a => a.UserId == _flagUserDetails.UserId)
            .AsNoTracking()
            .ToListAsync();

        return response.Select(a => new FlagDto()
        {
            IsEnabled = a.Value,
            Name = a.Name
        }).ToList();
    }
}

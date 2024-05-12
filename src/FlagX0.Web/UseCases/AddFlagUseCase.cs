using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using FlagX0.Web.Services;

namespace FlagX0.Web.UseCases;

public class AddFlagUseCase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IFlagUserDetails _flagUserDetails;

    public AddFlagUseCase(ApplicationDbContext applicationDbContext, IFlagUserDetails flagUserDetails)
    {
        _applicationDbContext = applicationDbContext;
        _flagUserDetails = flagUserDetails;
    }

    public async Task<bool> Execute(string name, bool isEnabled)
    {
        FlagEntity flagEntity = new FlagEntity 
        { 
            Name = name,
            UserId = _flagUserDetails.UserId, 
            Value = isEnabled
        };
        var response = await _applicationDbContext.Flags.AddAsync(flagEntity);
        await _applicationDbContext.SaveChangesAsync();

        return true;
    }
}

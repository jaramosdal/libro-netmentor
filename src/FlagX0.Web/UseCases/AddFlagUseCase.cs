using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using FlagX0.Web.Services;
using Microsoft.EntityFrameworkCore;
using ROP;

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

    public async Task<Result<bool>> Execute(string flagName, bool isActive)
        => await ValidateFlag(flagName).Bind(x => ADdFlagToDatabase(x, isActive));

    private async Task<Result<string>> ValidateFlag(string flagName)
    {
        bool flagExist= await _applicationDbContext.Flags
            .Where(a => 
                a.UserId == _flagUserDetails.UserId && 
                a.Name.Equals(flagName, StringComparison.InvariantCultureIgnoreCase))
            .AnyAsync();

        if (flagExist)
        {
            return Result.Failure<string>("Flag Name Already Exist");
        }

        return flagName;
    }

    private async Task<Result<bool>> ADdFlagToDatabase(string flagName, bool isActive)
    {
        FlagEntity flagEntity = new FlagEntity
        {
            Name = flagName,
            UserId = _flagUserDetails.UserId,
            Value = isActive
        };
        _ = await _applicationDbContext.Flags.AddAsync(flagEntity);
        await _applicationDbContext.SaveChangesAsync();

        return true;
    }
}

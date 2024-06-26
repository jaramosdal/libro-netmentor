﻿using FlagX0.UnitTests.Web.Business.UserInfo;
using FlagX0.Web.Business.UseCases.Flags;
using FlagX0.Web.Business.UserInfo;
using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace FlagX0.UnitTests.Web.Business.UseCases.Flags;

public class AddFlagUseCaseTest
{
    [Fact]
    public async Task WhenFlagNameAlreadyExists_ThenError()
    {
        // Arrange
        IFlagUserDetails flagUserDetails = new FlagUserDetailsStub();
        ApplicationDbContext inMemoryDb = GetInMemoryDbContext(flagUserDetails);

        FlagEntity currentFlag = new FlagEntity
        {
            UserId = flagUserDetails.UserId,
            Name = "name",
            Value = true
        };

        inMemoryDb.Flags.Add(currentFlag);
        await inMemoryDb.SaveChangesAsync();

        // Act
        AddFlagUseCase addFlagUseCase = new AddFlagUseCase(inMemoryDb, flagUserDetails);
        var result = await addFlagUseCase.Execute(currentFlag.Name, true);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Flag Name Already Exist", result.Errors.First().Message);
    }

    [Fact]
    public async Task WhenFlagDoesNotExists_ThenInsertOnDb()
    {
        // Arrange
        IFlagUserDetails flagUserDetails = new FlagUserDetailsStub();
        ApplicationDbContext inMemoryDb = GetInMemoryDbContext(flagUserDetails);

        // Act
        AddFlagUseCase addFlagUseCase = new AddFlagUseCase(inMemoryDb, flagUserDetails);
        var result = await addFlagUseCase.Execute("flagName", true);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Value);
    }

    private ApplicationDbContext GetInMemoryDbContext(IFlagUserDetails flagUserDetails)
    {
        DbContextOptions<ApplicationDbContext> databaseOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("flagx0Db")
            .Options;

        return new ApplicationDbContext(databaseOptions, flagUserDetails);
    }
}

﻿using FlagX0.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlagX0.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<FlagEntity> Flags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

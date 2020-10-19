using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using raktarProgram.Repositories;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;
using IdentityUser = Microsoft.AspNet.Identity.EntityFramework.IdentityUser;

namespace raktarProgram.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class AddingRoles : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {
                    new IdentityRole {
                    Id = "admin",
                    Name = "Admin",
                    NormalizedName = "ADMIN" },

                    new IdentityRole {
                    Id = "leader",
                    Name = "Leader",
                    NormalizedName = "LEADER" },

                    new IdentityRole {
                    Id = "visitor",
                    Name = "Visitor",
                    NormalizedName = "VISITOR" },

            });

            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "1",
                    Email = "bado.mate@outlook.com",
                    UserName = "bado.mate@outlook.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "temporarypass"),
                    
                });

            builder.Entity<IdentityUserRole>().HasData(
                new IdentityUserRole
                {
                    RoleId = "admin",
                    UserId = "1"
                });
        }
    }
}

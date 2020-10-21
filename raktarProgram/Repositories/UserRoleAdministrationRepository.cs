using System.Collections.Generic;
using System.Linq;
using raktarProgram.Interfaces;
using raktarProgram.Data;
using Microsoft.EntityFrameworkCore;
using raktarProgram.Data.Filters;
using System.Threading.Tasks;
using raktarProgram.Helpers;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Cryptography;
using raktarProgram.Data.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Identity;
using System.Security.Permissions;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore.Query.Internal;
using raktarProgram.Data.Structs;

namespace raktarProgram.Repositories
{
    public class UserRoleAdministrationRepository : IUserRoleAdministrationRepository
    {
        private ApplicationDbContext context;
        public UserRoleAdministrationRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<IdentityRole> Roles => this.context.Roles;
        public IQueryable<IdentityRoleClaim<string>> RoleClaims => this.context.RoleClaims;
        public IQueryable<IdentityUser> Users => this.context.Users;
        public IQueryable<IdentityUserClaim<string>> UserClaims => this.context.UserClaims;
        public IQueryable<IdentityUserLogin<string>> UserLogins => this.context.UserLogins;
        public IQueryable<IdentityUserRole<string>> UserRoles => this.context.UserRoles;
        public IQueryable<IdentityUserToken<string>> UserTokens => this.context.UserTokens;

        public async Task<ListResult<IdentityUser>> ListUsers()
        {
            var lista = this.Users;

            ListResult<IdentityUser> res = new ListResult<IdentityUser>();

            res.Total = lista.Count();

            res.Data = await lista
                    .OrderBy(c => c.UserName)
                    .ToListAsync();

            return res;
        }

        public async Task<ListResult<RoleAndUserStruct>> ListUserRoles()
        {
            List<RoleAndUserStruct> list = new List<RoleAndUserStruct>();
            
             list = await UserRoles
                .Join(Users,
                p => p.UserId,
                o => o.Id,
                (p, e) => new RoleAndUserStruct(p, e))
                    .ToListAsync();

            ListResult<RoleAndUserStruct> res = new ListResult<RoleAndUserStruct>();

            res.Total = list.Count();


            res.Data = list;
            return res;

        }

        public async Task<ListResult<IdentityRole>> ListRoles()
        {
            List<IdentityRole> list = new List<IdentityRole>();
            list = await this.Roles.ToListAsync();

            ListResult<IdentityRole> res = new ListResult<IdentityRole>();

            res.Total = list.Count();
            res.Data = list
                    .OrderBy(c => c.Id)
                    .ToList();

            return res;
        }

        public async Task<RoleAndUserStruct> RoleModositas(RoleAndUserStruct roleAndUser)
        {
            IdentityUserRole<string> updatedRole = new IdentityUserRole<string>();
            updatedRole.UserId = this.context.Users.Where(c => c.Id == roleAndUser.User.Id).First().Id;
            updatedRole.RoleId = this.context.Roles.Where(c => c.Id == roleAndUser.RoleId).First().Id;

            if (roleAndUser.Role != null)
            {
                this.context.UserRoles.Remove(roleAndUser.Role);
                this.context.SaveChanges();
            }

            this.context.UserRoles.Add(updatedRole);
            this.context.SaveChanges();

            return roleAndUser;

        }
             
    }
}

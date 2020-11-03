using Microsoft.AspNetCore.Identity;
using raktarProgram.Data.Filters;
using raktarProgram.Data.Structs;
using raktarProgram.Helpers;
using raktarProgram.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raktarProgram.Interfaces
{
    public interface IUserRoleAdministrationRepository
    {
        IQueryable<IdentityRole> Roles { get; }
        IQueryable<IdentityRoleClaim<string>> RoleClaims { get; }
        IQueryable<IdentityUser> Users { get; }
        IQueryable<IdentityUserClaim<string>> UserClaims { get; }
        IQueryable<IdentityUserLogin<string>> UserLogins { get; }
        IQueryable<IdentityUserRole<string>> UserRoles { get; }
        IQueryable<IdentityUserToken<string>> UserTokens { get; }

        Task<ListResult<IdentityRole>> ListRoles();
        Task<ListResult<RoleAndUserStruct>> ListUserRoles(UserRoleAdministrationFilter filter, int pageSize, int pageNum);
        Task<ListResult<IdentityUser>> ListUsers();
        Task<RoleAndUserStruct> RoleModositas(RoleAndUserStruct roleAndUser);
    }
}

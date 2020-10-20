using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Configuration;

namespace raktarProgram.Data.Structs
{
    public struct RoleAndUserStruct
    {
        public IdentityUserRole<string> Role;
        public IdentityUser User;

        public RoleAndUserStruct(IdentityUserRole<string> role, IdentityUser user)
        {
            Role = role;
            User = user;
        }
         
        public override bool Equals(object? obj)
        {
            return obj is RoleAndUserStruct other &&
                   EqualityComparer<IdentityUserRole<string>>.Default.Equals(Role, other.Role) &&
                   EqualityComparer<IdentityUser>.Default.Equals(User, other.User);
        }

        public bool Equal(object obj)
        {
            return obj is RoleAndUserStruct other &&
                   EqualityComparer<IdentityUserRole<string>>.Default.Equals(Role, other.Role) &&
                   EqualityComparer<IdentityUser>.Default.Equals(User, other.User); ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Role, User);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void Deconstruct(out IdentityUserRole<string> role, out IdentityUser user)
        {
            role = Role;
            user = User;
        }

        public static implicit operator (IdentityUserRole<string> Role, IdentityUser User)(RoleAndUserStruct value)
        {
            return (value.Role, value.User);
        }

        public static implicit operator RoleAndUserStruct((IdentityUserRole<string> Role, IdentityUser User) value)
        {
            return new RoleAndUserStruct(value.Role, value.User);
        }
    }
}

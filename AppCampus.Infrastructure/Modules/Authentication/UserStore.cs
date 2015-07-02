using AppCampus.Domain.Models.Identity;
using AppCampus.Infrastructure.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Infrastructure.Modules.Authentication
{
    public class UserStore : IUserStore<ApplicationUser, Guid>, IUserPasswordStore<ApplicationUser, Guid>, IUserRoleStore<ApplicationUser, Guid>, IQueryableUserStore<ApplicationUser, Guid>, IUserClaimStore<ApplicationUser, Guid>
    {
        public UserStore()
        {
        }

        public IQueryable<ApplicationUser> Users
        {
            get
            {
                using (var context = new AppCampusContext())
                {
                    return context.Users.ToList().Select(u => ApplicationUser.Hydrate(u.Id, u.CompanyId, u.UserName, u.FirstName, u.LastName, u.PasswordHash)).AsQueryable();
                }
            }
        }

        public Task CreateAsync(ApplicationUser user)
        {
            var dataEntity = new UserTable()
            {
                Id = user.Id,
                CompanyId = user.CompanyId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash
            };

            using (var context = new AppCampusContext())
            {
                context.Users.Add(dataEntity);
                context.SaveChanges();
            }

            return Task.FromResult<int>(0);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            UserTable dataEntity;

            using (var context = new AppCampusContext())
            {
                dataEntity = context.Users.Find(userId);
            }

            if (dataEntity == null)
            {
                return Task.FromResult<ApplicationUser>(null);
            }

            var user = ApplicationUser.Hydrate(dataEntity.Id, dataEntity.CompanyId, dataEntity.UserName, dataEntity.FirstName, dataEntity.LastName, dataEntity.PasswordHash);

            return Task.FromResult<ApplicationUser>(user);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            UserTable dataEntity;

            using (var context = new AppCampusContext())
            {
                dataEntity = context.Users.SingleOrDefault(x => x.UserName == userName);
            }

            if (dataEntity == null)
            {
                return Task.FromResult<ApplicationUser>(null);
            }

            var user = ApplicationUser.Hydrate(dataEntity.Id, dataEntity.CompanyId, dataEntity.UserName, dataEntity.FirstName, dataEntity.LastName, dataEntity.PasswordHash);

            return Task.FromResult<ApplicationUser>(user);
        }

        public IReadOnlyCollection<ApplicationUser> GetByCompany(Guid companyId)
        {
            List<UserTable> dataEntities;

            using (var context = new AppCampusContext())
            {
                dataEntities = context.Users.Where(u => u.CompanyId == companyId).ToList();
            }

            return dataEntities.Select(u => ApplicationUser.Hydrate(u.Id, u.CompanyId, u.UserName, u.FirstName, u.LastName, u.PasswordHash)).ToList();
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            using (var context = new AppCampusContext())
            {
                var dataEntity = context.Users.Find(user.Id);
                dataEntity.FirstName = user.FirstName;
                dataEntity.LastName = user.LastName;
                dataEntity.PasswordHash = user.PasswordHash;

                context.SaveChanges();
            }
            return Task.FromResult<int>(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult<bool>(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SetPasswordHash(passwordHash);

            return Task.FromResult<int>(0);
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            using (var context = new AppCampusContext())
            {
                var role = context.Roles.SingleOrDefault(r => r.Name.ToUpper() == roleName.ToUpper());

                if (role == null)
                {
                    throw new InvalidOperationException(String.Format("Role name {0} does not exist.", roleName));
                }

                var userRole = new UserRoleTable();
                userRole.Id = Guid.NewGuid();
                userRole.RoleId = role.Id;
                userRole.UserId = user.Id;

                context.UserRoles.Add(userRole);
                context.SaveChanges();
            }

            return Task.FromResult<int>(0);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            List<string> roles;

            using (var context = new AppCampusContext())
            {
                roles = context.Users.Find(user.Id).UserRoles.Select(ur => ur.Role).Select(r => r.Name).ToList();
            }

            return Task.FromResult<IList<string>>(roles);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            bool isInRole;

            using (var context = new AppCampusContext())
            {
                isInRole = context.Users.Find(user.Id).UserRoles.Select(ur => ur.Role).Any(r => r.Name == roleName);
            }

            return Task.FromResult<bool>(isInRole);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            using (var context = new AppCampusContext())
            {
                var role = context.Roles.SingleOrDefault(r => r.Name == roleName);

                var userEntity = context.Users.Find(user.Id);

                var userRole = userEntity.UserRoles.SingleOrDefault(ur => ur.RoleId == role.Id);

                context.UserRoles.Remove(userRole);

                context.SaveChanges();
            }

            return Task.FromResult<int>(0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public Task AddClaimAsync(ApplicationUser user, Claim claim)
        {
            using (var context = new AppCampusContext())
            {
                var userEntity = context.Users.Find(user.Id);

                if (userEntity == null)
                {
                    throw new InvalidOperationException("User does not exist.");
                }

                var userClaim = new UserClaimTable()
                {
                    Id = CombIdentityFactory.GenerateIdentity(),
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                };

                userEntity.UserClaims.Add(userClaim);

                context.SaveChanges();
            }

            return Task.FromResult<int>(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            using (var context = new AppCampusContext())
            {
                var userEntity = context.Users.Find(user.Id);

                if (userEntity == null)
                {
                    throw new InvalidOperationException("User does not exist.");
                }

                var claims = userEntity.UserClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();

                return Task.FromResult<IList<Claim>>(claims);
            }
        }

        public Task RemoveClaimAsync(ApplicationUser user, Claim claim)
        {
            using (var context = new AppCampusContext())
            {
                var userEntity = context.Users.Find(user.Id);

                if (userEntity == null)
                {
                    throw new InvalidOperationException("User does not exist.");
                }

                var claimEntity = userEntity.UserClaims.SingleOrDefault(c => c.ClaimType == claim.ValueType);
                userEntity.UserClaims.Remove(claimEntity);

                context.SaveChanges();
            }

            return Task.FromResult<int>(0);
        }
    }
}
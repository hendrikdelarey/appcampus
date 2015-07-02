using AppCampus.Domain.Models.Identity;
using AppCampus.Infrastructure.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppCampus.Infrastructure.Modules.Authentication
{
    public class RoleStore : IRoleStore<ApplicationRole, Guid>, IQueryableRoleStore<ApplicationRole, Guid>
    {
        public IQueryable<ApplicationRole> Roles
        {
            get
            {
                using (var context = new AppCampusContext())
                {
                    return context.Roles.ToList().Select(r => new ApplicationRole(r.Id, r.Name)).AsQueryable();
                }
            }
        }

        public RoleStore()
        {
        }

        public Task CreateAsync(ApplicationRole role)
        {
            var dataEntity = new RoleTable()
            {
                Id = role.Id,
                Name = role.Name
            };

            using (var context = new AppCampusContext())
            {
                context.Roles.Add(dataEntity);
                context.SaveChanges();
            }

            return Task.FromResult<int>(0);
        }

        public Task DeleteAsync(ApplicationRole role)
        {
            using (var context = new AppCampusContext())
            {
                var entity = context.Roles.Find(role.Id);
                context.Roles.Remove(entity);
                return context.SaveChangesAsync();
            }
        }

        public Task<ApplicationRole> FindByIdAsync(Guid roleId)
        {
            RoleTable dataEntity;

            using (var context = new AppCampusContext())
            {
                dataEntity = context.Roles.Find(roleId);
            }

            if (dataEntity == null)
            {
                return Task.FromResult<ApplicationRole>(null);
            }

            var role = new ApplicationRole(dataEntity.Id, dataEntity.Name);

            return Task.FromResult<ApplicationRole>(role);
        }

        public Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            RoleTable dataEntity;

            using (var context = new AppCampusContext())
            {
                dataEntity = context.Roles.SingleOrDefault(r => r.Name == roleName);
            }

            if (dataEntity == null)
            {
                return Task.FromResult<ApplicationRole>(null);
            }

            var role = new ApplicationRole(dataEntity.Id, dataEntity.Name);

            return Task.FromResult<ApplicationRole>(role);
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            using (var context = new AppCampusContext())
            {
                var dataEntity = context.Roles.Find(role.Id);
                dataEntity.Name = role.Name;

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
    }
}
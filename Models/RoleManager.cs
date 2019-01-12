using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace customeIdentity.Models
{
    public class RoleManager<TRole> where TRole : Role
    {
        private IRoleStore<TRole> Store { get; set; }
        public ILookupNormalizer KeyNormalizer { get; set; }
        private CancellationToken CancellationToken => CancellationToken.None;
        public RoleManager(IRoleStore<TRole> store, ILookupNormalizer keyNormalizer)
        {
            Store = store;
        }
        public async Task<IdentityResult> CreateAsync(TRole role)
        {
            await UpdateNormalizedRoleNameAsync(role);
            var result = await Store.CreateAsync(role, CancellationToken);
            return result;
        }

        public virtual async Task UpdateNormalizedRoleNameAsync(TRole role)
        {
            var name = await GetRoleNameAsync(role);
            await Store.SetNormalizedRoleNameAsync(role, NormalizeKey(name), CancellationToken);
        }

        public virtual Task<string> GetRoleNameAsync(TRole role)
        {
            // ThrowIfDisposed();
            return Store.GetRoleNameAsync(role, CancellationToken);
        }

        public virtual string NormalizeKey(string key)
        {
            return (KeyNormalizer == null) ? key : KeyNormalizer.Normalize(key);
        }
    }
}
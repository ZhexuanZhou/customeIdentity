using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace customeIdentity.Models
{
    public class UserManager<TUser> where TUser : User
    {
        private IUserStore<TUser> Store;

        public IPasswordHasher<TUser> PasswordHasher { get; set; }
        protected virtual CancellationToken CancellationToken => CancellationToken.None;
        public ILookupNormalizer KeyNormalizer { get; set; }
        public UserManager(IPasswordHasher<TUser> passwordHasher, IUserStore<TUser> store, ILookupNormalizer keyNormalizer)
        {
            Store = store;
            PasswordHasher = passwordHasher;
            KeyNormalizer = keyNormalizer;
        }

        private async Task<IdentityResult> UpdatePasswordHash(IUserPasswordStore<TUser> passwordStore,
            TUser user, string newPassword, bool validatePassword = true)
        {
        
            var hash = newPassword != null ? PasswordHasher.HashPassword(user, newPassword) : null;
            await passwordStore.SetPasswordHashAsync(user, hash, CancellationToken);
            // await UpdateSecurityStampInternal(user);
            return IdentityResult.Success;
        }
        public virtual async Task<IdentityResult> CreateAsync(TUser user, string password)
        {
            // ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            var result = await UpdatePasswordHash(passwordStore, user, password);
            if (!result.Succeeded)
            {
                return result;
            }
            return await CreateAsync(user);
        }
        public virtual async Task<IdentityResult> CreateAsync(TUser user)
        {
            // ThrowIfDisposed();
            // await UpdateSecurityStampInternal(user);
            // var result = await ValidateUserAsync(user);
            // if (!result.Succeeded)
            // {
            //     return result;
            // }
            // if (Options.Lockout.AllowedForNewUsers && SupportsUserLockout)
            // {
            //     await GetUserLockoutStore().SetLockoutEnabledAsync(user, true, CancellationToken);
            // }
            // await UpdateNormalizedUserNameAsync(user);
            // await UpdateNormalizedEmailAsync(user);

            return await Store.CreateAsync(user, CancellationToken);
        }
        private IUserPasswordStore<TUser> GetPasswordStore()
        {
            var cast = Store as IUserPasswordStore<TUser>;
            if (cast == null)
            {
                throw new NotSupportedException("error");
            }
            return cast;
        }

        public virtual async Task<TUser> FindByNameAsync(string userName)
        {
            var user = await Store.FindByNameAsync(userName, CancellationToken);

            return user;
        }

        public virtual async Task<IdentityResult> AddToRoleAsync(TUser user, string role)
        {
            // ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var normalizedRole = NormalizeKey(role);
            // if (await userRoleStore.IsInRoleAsync(user, normalizedRole, CancellationToken))
            // {
            //     return await UserAlreadyInRoleError(user, role);
            // }
            await userRoleStore.AddToRoleAsync(user, normalizedRole, CancellationToken);
            return await UpdateUserAsync(user);
        }
        private IUserRoleStore<TUser> GetUserRoleStore()
        {
            var cast = Store as IUserRoleStore<TUser>;
            if (cast == null)
            {
                throw new NotSupportedException("fadfasfs");
            }
            return cast;
        }
        public virtual string NormalizeKey(string key)
        {
            return (KeyNormalizer == null) ? key : KeyNormalizer.Normalize(key);
        }
        protected virtual async Task<IdentityResult> UpdateUserAsync(TUser user)
        {
            return await Store.UpdateAsync(user, CancellationToken);
        }
    }
}
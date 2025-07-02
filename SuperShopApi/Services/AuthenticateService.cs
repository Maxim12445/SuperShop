
using Microsoft.AspNetCore.Identity;
using SuperShopApi.Models;

namespace SuperShopApi.Services
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password,
                false, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

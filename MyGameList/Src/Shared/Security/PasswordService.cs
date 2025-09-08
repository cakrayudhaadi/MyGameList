using Microsoft.AspNetCore.Identity;
using MyGameList.Src.Features.Users.Models;

namespace MyGameList.Src.Shared.Security
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword);
    }

    public class PasswordService(IPasswordHasher<IdentityUser> passwordHasher) : IPasswordService
    {
        public string HashPassword(string password)
        {
            var user = new IdentityUser();
            return passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
        {
            var user = new IdentityUser();
            return passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
    }
}

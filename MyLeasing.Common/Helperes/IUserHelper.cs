

using Microsoft.AspNetCore.Identity;
using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Models;
using System.Threading.Tasks;

namespace MyLeasing.Common.Helperes
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);


        Task<IdentityResult> AddUserAsync(User user, string password);

       

        Task<IdentityResult> DeletAsync(User user);






        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();


        Task<IdentityResult> UpdateUserAsync(User user);


        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);










        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);
    }
}

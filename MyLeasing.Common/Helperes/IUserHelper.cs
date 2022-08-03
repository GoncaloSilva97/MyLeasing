

using Microsoft.AspNetCore.Identity;
using MyLeasing.Common.Data.Entities;
using System.Threading.Tasks;

namespace MyLeasing.Common.Helperes
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);


        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> DeletAsync(User user);



    }
}

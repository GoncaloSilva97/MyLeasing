

using Microsoft.AspNetCore.Identity;
using MyLeasing.Common.Data;
using MyLeasing.Common.Data.Entities;
using System.Threading.Tasks;

namespace MyLeasing.Common.Helperes
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;


        private readonly DataContext _context;


        public UserHelper(UserManager<User> userManager, DataContext context)
        {
            _userManager = userManager;


            _context = context;
        }



        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }




        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }





        public async Task<IdentityResult> DeletAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }


        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);

            
        }

    }
}

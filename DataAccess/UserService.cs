using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.Data;
using StaffTraffic.Models.Entities;

namespace StaffTraffic.DataAccess
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        public UserService(UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of UserApplication " +
                    $"Ensure that UserApplication is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        public async Task<Guid?> Create(ApplicationUser newUser)
        {
            var user = CreateUser();
            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.Email = newUser.Email;
            user.IsEnable = newUser.IsEnable;
            user.PhoneNumber = newUser.PhoneNumber;
            await _userStore.SetUserNameAsync(user, newUser.UserName.ToString(), CancellationToken.None);
            await _emailStore.SetEmailAsync(user, newUser.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, "Abc123@");

            if (!result.Succeeded)
            {
                return null;
            }


            var userId = await _userManager.GetUserIdAsync(user);
            return new Guid(userId);
        }
        //public async Task<bool> Update(ApplicationUser user)
        //{
        //    var userTarget = await _userManager.UserBy(user);
        //}
        //public async Task<bool> Delete(Traffic traffic)
        //{
        //    var trafficTarget = await _context.Traffic.AsNoTracking().FirstOrDefaultAsync(x => x.Id == traffic.Id);
        //    if (traffic is null)
        //    {
        //        return false;
        //    }
        //    _context.Traffic.Remove(traffic);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}
        public async Task<ApplicationUser?> GetById(Guid id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
        public async Task<List<ApplicationUser>> Get()
        {

            var users = await _userManager.Users.ToListAsync();
            return users;
        }

    }
}

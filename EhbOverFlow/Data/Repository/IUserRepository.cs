using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Models;

namespace EhbOverFlow.Data.Repository
{
    public interface IUserRepository
    {

        void AddUser(ApplicationUser user);
        List<ApplicationUser> GetAllUser();
        ApplicationUser GetUser(string id);
        void RemoveUser(string id);
        void UpdateUser(ApplicationUser user);
        Task<bool> SaveChangesAsync();
    }
}

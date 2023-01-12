using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Models;

namespace EhbOverFlow.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _ehbOverFlowDbContext;
        public UserRepository(ApplicationDbContext ehbOverFlowDbContext) 
        {
            _ehbOverFlowDbContext = ehbOverFlowDbContext;
        }

        public void AddUser(ApplicationUser user)
        {
           _ehbOverFlowDbContext.applicationUsers.Add(user);
        }

        public List<ApplicationUser> GetAllUser()
        {
            return _ehbOverFlowDbContext.applicationUsers.ToList();
        }

        public ApplicationUser GetUser(string id)
        {
            return _ehbOverFlowDbContext.applicationUsers.FirstOrDefault(u => u.Id == id);
        }

        public void RemoveUser(string id)
        {
            _ehbOverFlowDbContext.Remove(GetUser(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ehbOverFlowDbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void UpdateUser(ApplicationUser user)
        {
            _ehbOverFlowDbContext.Update(user);
        }
    }
}

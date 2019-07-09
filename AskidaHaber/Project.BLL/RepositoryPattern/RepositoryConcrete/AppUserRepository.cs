using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.MODEL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Project.BLL.RepositoryPattern.RepositoryConcrete
{
    public class AppUserRepository : BaseRepository<AppUser>
    {
        public List<AppUser> SelectBanUsers()
        {
            return db.Set<AppUser>().Where(x => x.IsBanned == true).ToList();
        }
    }
}

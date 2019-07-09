using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.MODEL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Project.BLL.RepositoryPattern.RepositoryConcrete
{
    public class ColumnistRepository : BaseRepository<Columnist>
    {
        public List<Columnist> SelectBanUsers()
        {
            return db.Set<Columnist>().Where(x => x.IsBanned == true).ToList();
        }
    }
}

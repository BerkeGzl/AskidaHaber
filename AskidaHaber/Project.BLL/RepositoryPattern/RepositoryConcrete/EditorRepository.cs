using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.MODEL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Project.BLL.RepositoryPattern.RepositoryConcrete
{
    public class EditorRepository : BaseRepository<Editor>
    {
        public List<Editor> SelectBanUsers()
        {
            return db.Set<Editor>().Where(x => x.IsBanned == true).ToList();
        }
    }
}

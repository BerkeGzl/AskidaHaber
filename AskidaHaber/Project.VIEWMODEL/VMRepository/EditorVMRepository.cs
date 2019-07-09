using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.VIEWMODEL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMRepository
{
    public class EditorVMRepository : BaseRepository<EditorVM>
    {
        public override List<EditorVM> SelectActives()
        {
            return db.Editors.Select(x => new EditorVM
            {
                ID = x.ID,
                UserName = x.UserName,
                Password = x.Password,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                BirthDate = x.BirthDate,
                AboutDescription = x.AboutDescription
            }).ToList();
        }
    }
}

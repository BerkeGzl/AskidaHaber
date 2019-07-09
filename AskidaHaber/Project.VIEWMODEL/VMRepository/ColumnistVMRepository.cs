using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.VIEWMODEL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMRepository
{
    public class ColumnistVMRepository : BaseRepository<ColumnistVM>
    {
        public override List<ColumnistVM> SelectActives()
        {
            return db.Columnists.Select(x => new ColumnistVM
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

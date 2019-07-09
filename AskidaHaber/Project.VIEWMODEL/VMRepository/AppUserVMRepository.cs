using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.VIEWMODEL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMRepository
{
    public class AppUserVMRepository : BaseRepository<AppUserVM>
    {
        public override List<AppUserVM> SelectActives()
        {
            return db.AppUsers.Select(x => new AppUserVM
            {
                ID = x.ID,
                UserName = x.UserName,
                Password = x.Password,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                BirthDate = x.BirthDate,
               UserIP = x.UserIP,
               AboutDescription = x.AboutDescription
            }).ToList();
        }
    }
}

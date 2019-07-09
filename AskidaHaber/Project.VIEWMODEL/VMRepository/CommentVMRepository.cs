using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.VIEWMODEL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMRepository
{
    public class CommentVMRepository : BaseRepository<CommentVM>
    {
        public override List<CommentVM> SelectActives()
        {
            return db.Comments.Select(x => new CommentVM
            {
                ID = x.ID,
                Text = x.Text,
                AppUserID = x.AppUserID,
                ArticleID = x.ArticleID,
                NewsID = x.NewsID
            }).ToList();
        }
    }
}

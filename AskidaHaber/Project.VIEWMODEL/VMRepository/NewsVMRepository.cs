using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.VIEWMODEL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMRepository
{
    public class NewsVMRepository : BaseRepository<NewsVM>
    {
        public override List<NewsVM> SelectActives()
        {
            return db.News.Select(x => new NewsVM
            {
                ID = x.ID,
                Title = x.Title,
                Summary = x.Summary,
                Content = x.Content,
                Quotation = x.Quotation,
                CategoryID = x.CategoryID,
                EditorID = x.EditorID
            }).ToList();
        }
    }
}

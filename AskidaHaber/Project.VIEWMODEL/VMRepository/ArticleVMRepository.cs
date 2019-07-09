using Project.BLL.RepositoryPattern.RepositoryBase;
using Project.VIEWMODEL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMRepository
{
    public class ArticleVMRepository : BaseRepository<ArticleVM>
    {
        public override List<ArticleVM> SelectActives()
        {
            return db.Articles.Select(x => new ArticleVM {
                ID = x.ID,
                Title = x.Title,
                Summary = x.Summary,
                Content = x.Content,
                Quotation = x.Quotation,
                CategoryID = x.CategoryID,
                EditorID = x.EditorID,
                ColumnistID = x.ColumnistID
            }).ToList();
        }
    }
}

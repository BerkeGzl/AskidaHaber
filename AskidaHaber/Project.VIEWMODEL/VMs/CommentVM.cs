using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMs
{
    public class CommentVM : BaseEntity
    {
        public string Text { get; set; }
        public int AppUserID { get; set; }
        public int? NewsID { get; set; }
        public int? ArticleID { get; set; }
    }
}

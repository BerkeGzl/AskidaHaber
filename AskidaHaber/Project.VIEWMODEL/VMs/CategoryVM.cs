using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMs
{
    public class CategoryVM : BaseEntity
    {
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}

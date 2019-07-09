using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMs
{
    public class AppUserVM : AccessClass
    {
        public string UserIP { get; set; }
    }
}

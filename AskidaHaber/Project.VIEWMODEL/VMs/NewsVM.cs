using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VIEWMODEL.VMs
{
    public class NewsVM : BodyClass
    {
        //MVC VM
        public List<News> karisik { get; set; }
        public List<News> magazin { get; set; }
        public List<News> ekonomi { get; set; }
        public List<News> saglik  { get; set; }
        public List<News> sonDakika { get; set; }
        public List<News> gundem { get; set; }
        public List<News> dunya { get; set; }

        //Property
        public string VideoPath { get; set; }

    }
}

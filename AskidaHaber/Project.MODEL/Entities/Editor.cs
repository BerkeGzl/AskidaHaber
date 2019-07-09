using System;
using System.Collections.Generic;

namespace Project.MODEL.Entities
{
    public class Editor : AccessClass
    {
        public Editor()
        {
            ResetPassword = Guid.NewGuid();
        }

        //Relational Properties
        public virtual List<News> News { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}

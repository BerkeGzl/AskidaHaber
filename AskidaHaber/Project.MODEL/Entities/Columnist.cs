using System;
using System.Collections.Generic;

namespace Project.MODEL.Entities
{
    public class Columnist : AccessClass
    {
        public Columnist()
        {
            ResetPassword = Guid.NewGuid();
        }

        //Relational Properties
        public virtual List<Article> Articles { get; set; }
    }
}

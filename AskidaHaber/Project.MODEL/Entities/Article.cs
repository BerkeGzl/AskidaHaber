using System.Collections.Generic;

namespace Project.MODEL.Entities
{
    public class Article : BodyClass
    {
        public int ColumnistID { get; set; }

        //Relational Properties
        public virtual Category Category { get; set; }

        public virtual Columnist Columnist { get; set; }

        public virtual Editor Editor { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}

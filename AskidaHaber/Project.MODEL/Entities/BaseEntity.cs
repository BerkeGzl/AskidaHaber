using Project.MODEL.Enums;
using System;

namespace Project.MODEL.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DataStatus Status { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public BaseEntity()
        {
            Status = DataStatus.Inserted;
            CreatedDate = DateTime.Now;
        }
    }
}

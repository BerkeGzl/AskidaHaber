using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.MODEL.Entities
{
    public abstract class PictureSpecific : BaseEntity
    {
        [Column("Dosya Yolu"), MaxLength(1000)]
        public string ImagePath { get; set; }
    }
}

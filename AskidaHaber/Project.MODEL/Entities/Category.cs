using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.MODEL.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage ="Lütfen Kategori Adını Giriniz"), MaxLength(50,ErrorMessage ="{0} alanına maksimum {1} karakter girebilirsiniz")]
        public string CategoryName { get; set; }

        [MaxLength(100, ErrorMessage ="{0} alanına maksimum {1} karakter girebilirsiniz")]
        public string Description { get; set; }

        //Relational Properties
        public virtual List<News> News { get; set; }

        public virtual List<Article> Articles { get; set; }

        public Category()
        {
            Articles = new List<Article>();
            News = new List<News>();
        }
    }
}

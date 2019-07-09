using System.ComponentModel.DataAnnotations;

namespace Project.MODEL.Entities
{
    public class Comment : BaseEntity
    {
        [Required(ErrorMessage ="Lütfen bir yorum giriniz"),MaxLength(250,ErrorMessage ="{0} alanına maksimum {1} karakter girebilirsiniz")]
        public string Text { get; set; }

        public int AppUserID { get; set; }

        public int? NewsID { get; set; }

        public int? ArticleID { get; set; }
        //Relational Properties
        public virtual AppUser AppUser { get; set; }

        public virtual News News { get; set; }

        public virtual Article Article { get; set; }
    }
}

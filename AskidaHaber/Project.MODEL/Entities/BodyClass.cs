using System.ComponentModel.DataAnnotations;

namespace Project.MODEL.Entities
{
    public abstract class BodyClass : PictureSpecific
    {
        [Required(ErrorMessage = "Lütfen başlık giriniz"), MaxLength(200, ErrorMessage = "{0} alanına en fazla {1} karakter girebilirsiniz")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Lütfen özet giriniz"), MaxLength(200, ErrorMessage = "{0} alanına en fazla {1} karakter girebilirsiniz")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Lütfen haber gövdesi giriniz"), MaxLength(3000, ErrorMessage = "{0} alanına en fazla {1} karakter girebilirsiniz")]
        public string Content { get; set; }

        [MaxLength(200, ErrorMessage ="{0} alanına en fazla {1} karakter girebilirsiniz")]
        public string Quotation { get; set; }

        public int? CategoryID { get; set; }

        public int? EditorID { get; set; }
    }
}

 using Project.MODEL.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.MODEL.Entities
{
    public abstract class AccessClass : PictureSpecific
    {
        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur"), MaxLength(20, ErrorMessage = "{0} alanına maksimum {1} karakter girebilirsiniz"), MinLength(5, ErrorMessage = "{0} alanına minimum {1} karakter girebilirsiniz"), Display(Name = "Kullanıcı Adı")]
        [Column("Kullanıcı İsmi"), StringLength(25)]
        public string UserName { get; set; }

        [Required(ErrorMessage ="{0} Bu alanın girilmesi zorunludur"), MaxLength(100, ErrorMessage ="{0} alanına maksimum {1} karakter girebilirsiniz"), Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        [Column("Şifre"), StringLength(20)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string RePassword { get; set; }

        [Required(ErrorMessage ="Lütfen mail adresinizi boş bırakmayın"),EmailAddress(ErrorMessage ="Geçerli bir email adresi giriniz")]
        [Column("E-Posta"), StringLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen isminizi giriniz"), MaxLength(30, ErrorMessage = "{0} alanına maksimum {1} karakter girebilirsiniz")]
        [Column("İsim"), StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz"), MaxLength(30, ErrorMessage = "{0} alanına maksimum {1} karakter girebilirsiniz")]
        [Column("Soyisim"), StringLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [MaxLength(100, ErrorMessage ="{0}, alanına maksimum {1} karakter girebilirsiniz")]
        [Column("Kişi Hakkında"), StringLength(100)]
        public string AboutDescription { get; set; }

        [Column("Cinsiyet")]
        public Gender? Gender { get; set; }

        [Column("Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }

        [Column("Yasaklımı")]
        public bool IsBanned { get; set; }

        public Guid? ResetPassword{ get; set; }

        public AccessClass()
        {
            IsBanned = false;
        }
    }
}

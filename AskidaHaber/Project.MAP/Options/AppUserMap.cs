using Project.MODEL.Entities;

namespace Project.MAP.Options
{
    public class AppUserMap : BaseMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("Kullanıcılar");
            Property(x => x.ID).HasColumnName("KullanıcıID");
            Property(x => x.Role).HasColumnName("Rolü");
            Property(x => x.ActivationCode).HasColumnName("Aktivasyon Kodu");
            Property(x => x.IsActive).HasColumnName("Aktifmi");
            Property(x => x.UserIP).HasColumnName("Kullanıcı IP").HasMaxLength(50);
            Property(x => x.ResetPassword).HasColumnName("Şifre Sıfırlama Kodu");
        }
    }
}
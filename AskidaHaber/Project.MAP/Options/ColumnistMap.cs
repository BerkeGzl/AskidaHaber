using Project.MODEL.Entities;

namespace Project.MAP.Options
{
    public class ColumnistMap : BaseMap<Columnist>
    {
        public ColumnistMap()
        {
            ToTable("Yazarlar");
            Property(x => x.ID).HasColumnName("YazarID");
            Property(x => x.ResetPassword).HasColumnName("Şifre Sıfırlama Kodu");
        }
    }
}

using Project.MODEL.Entities;

namespace Project.MAP.Options
{
    public class LogMap : BaseMap<Log>
    {
        public LogMap()
        {
            ToTable("Günlükler");
            Property(x => x.WhoIs).HasColumnName("Kullanıcı Adı");
            Property(x => x.ID).HasColumnName("GünlükID");
            Property(x => x.ActionName).HasColumnName("Action Adı");
            Property(x => x.ControllorName).HasColumnName("Controller Adı");
            Property(x => x.Description).HasColumnName("Açıklama");
            Property(x => x.Information).HasColumnName("Bilgi");
            Property(x => x.IPAdress).HasColumnName("IP Adresi");
            Property(x => x.UrlAccessed).HasColumnName("URL Erişimi");
        }
    }
}
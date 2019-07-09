using Project.MODEL.Entities;

namespace Project.MAP.Options
{
    public class EditorMap : BaseMap<Editor>
    {
        public EditorMap()
        {
            ToTable("Editörler");
            Property(x => x.ID).HasColumnName("EditörID");
            Property(x => x.ResetPassword).HasColumnName("Şifre Sıfırlama Kodu");
        }
    }
}

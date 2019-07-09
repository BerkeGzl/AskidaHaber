using Project.MODEL.Entities;

namespace Project.MAP.Options
{
    public class CommentMap : BaseMap<Comment>
    {
        public CommentMap()
        {
            ToTable("Yorumlar");
            Property(x => x.ID).HasColumnName("YorumID");
            Property(x => x.Text).IsOptional().HasColumnName("Metin").HasMaxLength(300);
            Property(x => x.AppUserID).HasColumnName("KullanıcıID");
            Property(x => x.NewsID).HasColumnName("HaberID").IsOptional();
            Property(x => x.ArticleID).HasColumnName("YazıID").IsOptional();
        }
    }
}

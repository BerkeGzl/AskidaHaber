using Project.MODEL.Entities;

namespace Project.MAP.Options
{
    public class ArticleMap : BaseMap<Article>
    {
        public ArticleMap()
        {
            ToTable("Yazılar");
            Property(x => x.ID).HasColumnName("YazıID");
            Property(x => x.CategoryID).HasColumnName("KategoriID");
            Property(x => x.EditorID).HasColumnName("EditörID").IsOptional();
            Property(x => x.ColumnistID).HasColumnName("YazarID");
            Property(x => x.Title).HasColumnName("Başlık").HasMaxLength(3000).IsRequired();
            Property(x => x.Summary).HasColumnName("Özet").HasMaxLength(200).IsRequired();
            Property(x => x.Content).HasColumnName("Gövde").HasMaxLength(3000).IsRequired();
            Property(x => x.Quotation).HasColumnName("Alıntı").HasMaxLength(200).IsOptional();
        }
    }
}

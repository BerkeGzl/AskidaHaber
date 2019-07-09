using Project.MODEL.Entities;
namespace Project.MAP.Options
{
    public class NewsMap : BaseMap<News>
    {
        public NewsMap()
        {
            ToTable("Haberler");
            Property(x => x.ID).HasColumnName("HaberID");
            Property(x => x.CategoryID).HasColumnName("KategoriID").IsOptional();
            Property(x => x.EditorID).HasColumnName("EditörID").IsOptional();
            Property(x => x.Title).HasColumnName("Başlık").HasMaxLength(3000).IsRequired();
            Property(x => x.Summary).HasColumnName("Özet").HasMaxLength(200).IsRequired();
            Property(x => x.Content).HasColumnName("Gövde").HasMaxLength(3000).IsRequired();
            Property(x => x.Quotation).HasColumnName("Alıntı").HasMaxLength(200).IsOptional();
            Property(x => x.VideoPath).HasColumnName("VideoYolu").HasMaxLength(1000);
        }
    }
}

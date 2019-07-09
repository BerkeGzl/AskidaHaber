using Project.MODEL.Entities;

namespace Project.MAP.Options
{
    public class CategoryMap : BaseMap<Category>
    {
        public CategoryMap()
        {
            ToTable("Kategoriler");
            Property(x => x.ID).HasColumnName("KategoriID");
            Property(x => x.CategoryName).HasColumnName("Kategori Adı").IsRequired();
            Property(x => x.Description).HasColumnName("Açıklama").IsOptional();
        }
    }
}

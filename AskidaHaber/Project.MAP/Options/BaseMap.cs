using Project.MODEL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Project.MAP.Options
{
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T: BaseEntity
    {
        public BaseMap()
        {
            Property(x => x.Status).HasColumnName("Veri Durumu");
            Property(x => x.CreatedDate).HasColumnName("Oluşturma Tarihi").HasColumnType("datetime2").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("Oluşturan").HasMaxLength(50);
            Property(x => x.ModifiedDate).HasColumnName("Güncelleme Tarihi").HasColumnType("datetime2").IsOptional();
            Property(x => x.ModifiedBy).HasColumnName("Güncelleyen").HasMaxLength(50);
        }
    }
}

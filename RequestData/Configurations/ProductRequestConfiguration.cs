using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestDomain.Entities;

namespace RequestData.Configurations
{
	public class ProductRequestConfiguration : IEntityTypeConfiguration<ProductRequest>
	{
		public void Configure(EntityTypeBuilder<ProductRequest> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).IsRequired();
			builder.Property(p => p.RequestId).IsRequired();
			builder.Property(p => p.ProductId).IsRequired();
			builder.Property(p => p.ProductDescription).IsRequired();
			builder.Property(p => p.ProductCategory).IsRequired();
			builder.Property(p => p.Quantity).IsRequired();
			builder.Property(p => p.Value).IsRequired();
			builder.Property(p => p.Total).IsRequired();
		}
	}
}

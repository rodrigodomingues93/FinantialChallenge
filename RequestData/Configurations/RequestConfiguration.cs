using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestDomain.Entities;

namespace RequestData.Configurations
{
	public class RequestConfiguration : IEntityTypeConfiguration<Request>
	{
		public void Configure(EntityTypeBuilder<Request> builder)
		{
			builder.HasKey(r => r.Id);
			builder.Property(r => r.Id).IsRequired();
			builder.Property(r => r.Code);
			builder.Property(r => r.Date);
			builder.Property(r => r.DeliveryDate);
			builder.HasMany(r => r.Products).WithOne(r => r.Request).HasForeignKey(r => r.RequestId);
			builder.Property(r => r.Client);
			builder.Property(r => r.ClientDescription);
			builder.Property(r => r.ClientEmail);
			builder.Property(r => r.ClientPhone);
			//builder.Property(r => r.Status);
			builder.Property(r => r.Street);
			//builder.Property(r => r.Number);
			//builder.Property(r => r.Sector);
			builder.Property(r => r.Complement);
			builder.Property(r => r.City);
			//builder.Property(r => r.State);
			builder.Property(r => r.ProductsValue);
			builder.Property(r => r.Discount);
			builder.Property(r => r.Cost);
			builder.Property(r => r.TotalValue);
		}
	}
}

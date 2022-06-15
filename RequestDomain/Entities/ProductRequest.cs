using Infrastructure.Base;
using RequestDomain.Entities.Enums;

namespace RequestDomain.Entities
{
	public class ProductRequest : EntityBase
	{
		//[ForeignKey("BuyRequest")]
		public Guid RequestId { get; set; }
		public Guid ProductId { get; set; } = Guid.NewGuid();
		public string ProductDescription { get; set; }
		public EnumProductCategory ProductCategory { get; set; }
		public decimal Quantity { get; set; }
		public decimal Value { get; set; }
		private decimal _total;
		public decimal Total
		{
			get { return _total; }
			set { _total = Convert.ToDecimal((Quantity * Value).ToString("N2")); }
		}

		public virtual Request Request { get; set; }
	}
}

using Infrastructure.Base;
using RequestDomain.Entities.Enums;
using RequestDomain.Validators;

namespace RequestDomain.Entities
{
	public class Request : EntityBase
	{
		public long Code { get; set; }
		public DateTimeOffset Date { get; set; }
		public DateTimeOffset DeliveryDate { get; set; }
		private List<ProductRequest> _products { get; set; } = new List<ProductRequest>();
		public Guid Client { get; set; }
		public string ClientDescription { get; set; }
		public string ClientEmail { get; set; }
		public string ClientPhone { get; set; }
		public EnumStatus Status { get; set; }
		public string Street { get; set; }
		public string Number { get; set; }
		public string Sector { get; set; }
		public string Complement { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public decimal ProductsValue { get; set; }
		public decimal Discount { get; set; }
		public decimal Cost { get; set; }
		public decimal TotalValue { get; set; }

		public List<ProductRequest> Products
		{
			get { return _products; }
			set { _products = value ?? new List<ProductRequest>(); }
		}
	}
}

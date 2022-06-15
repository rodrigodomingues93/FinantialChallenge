using DocumentDomain.Entities.Enums;
using Infrastructure.Base;

namespace DocumentDomain.Entities
{
	public class Document : EntityBase
	{
		private DateTimeOffset? _date;
		public string Number { get; set; }
		public DateTimeOffset Date { get; set; }
		public EnumDocumentType DocumentType { get; set; }
		public EnumOperation Operation { get; set; }
		public bool Paid { get; set; }
		public DateTimeOffset? PaymentDate
		{
			get
			{
				if (Paid == true)
				{
					return _date= DateTimeOffset.Now;
				}
				else 
				{
					return null;
				}
			}
			set
			{
				_date = value;
			}
		}
		public string Description { get; set; }
		public decimal Total { get; set; }
		public string Observations { get; set; }
	}
}

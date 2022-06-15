using DocumentDomain.Entities.Enums;

namespace DocumentDomain.DTO
{
	public class DocumentDto
	{
		public string Number { get; set; }
		public DateTimeOffset Date { get; set; }
		public EnumDocumentType DocumentType { get; set; }
		public EnumOperation Operation { get; set; }
		public bool Paid { get; set; }
		public DateTimeOffset PaymentDate { get; set; }
		public string Description { get; set; }
		public decimal Total { get; set; }
		public string Observations { get; set; }
	}
}

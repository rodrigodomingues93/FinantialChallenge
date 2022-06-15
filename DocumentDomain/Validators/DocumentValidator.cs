using DocumentDomain.Entities;
using DocumentDomain.Entities.Enums;
using FluentValidation;

namespace DocumentDomain.Validators
{
	public class DocumentValidator : AbstractValidator<Document>
	{
		public DocumentValidator()
		{
			RuleFor(d => d.Number).NotNull().WithMessage("Number can't be null");
			RuleFor(d => d.Date).NotNull().WithMessage("Date can't be null");
			RuleFor(d => d.DocumentType).NotNull().IsInEnum().WithMessage("DocumentType invalid");
			RuleFor(d => d.Operation).NotNull().IsInEnum().WithMessage("Operationinvalid");
			RuleFor(d => d.Paid).NotNull().WithMessage("Payment can´t be null");
			//RuleFor(d => d.PaymentDate).NotNull().WithMessage("PaymentDate can't be null");
			RuleFor(d => d.Description).NotNull().WithMessage("Description can't be null");
			RuleFor(d => d.Total).NotNull().WithMessage("Total can't be null")
				.GreaterThan(0).When(d => d.Operation == EnumOperation.Entry, ApplyConditionTo.CurrentValidator).WithMessage("Total must be positive.")
				.LessThan(0).When(d => d.Operation == EnumOperation.Exit, ApplyConditionTo.CurrentValidator).WithMessage("Total must be negative.");
			//RuleFor(d => d.Observations).NotNull().WithMessage("Obs can't be null");

		}
	}
}

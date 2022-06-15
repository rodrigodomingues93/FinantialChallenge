using FluentValidation;
using RequestDomain.Entities;

namespace RequestDomain.Validators
{
	public class RequestValidator : AbstractValidator<Request>
	{
		public RequestValidator()
		{
			//RuleFor(r => r.Id).NotNull().WithMessage("Id can't be null");
			RuleFor(r => r.Code).NotNull().WithMessage("Code can't be null");
			RuleFor(r => r.Date).NotNull().WithMessage("Date can't be null");
			//RuleFor(r => r.Products).Must(r => r.GroupBy(p => p.ProductCategory).Count() == 1)
			//	.WithMessage("The Request can't have different categories of products");
			//RuleFor(r => r.DeliveryDate).NotNull().WithMessage("DeliveryDate can't be null");
			//RuleFor(r => r.Client).NotNull().WithMessage("Client can't be null");
			RuleFor(r => r.ClientDescription).NotNull().WithMessage("ClientDescription can't be null");
			RuleFor(r => r.ClientEmail).EmailAddress().NotEmpty().WithMessage("Email can't be null");
			RuleFor(r => r.ClientPhone).NotNull().WithMessage("ClientPhone can't be null");
			RuleFor(r => r.Status).NotNull().WithMessage("Status can't be null").IsInEnum().WithMessage("Status not valid");
			//RuleFor(r => r.Street).NotNull().WithMessage("Street can't be null");
			//RuleFor(r => r.Number).NotNull().WithMessage("Number can't be null");
			//RuleFor(r => r.Sector).NotNull().WithMessage("Sector can't be null");
			//RuleFor(r => r.Complement).NotNull().WithMessage("Complement can't be null");
			//RuleFor(r => r.City).NotNull().WithMessage("City can't be null");
			//RuleFor(r => r.State).NotNull().WithMessage("State can't be null");
			RuleFor(r => r.ProductsValue).NotNull().WithMessage("ProductsValue can't be null");
			RuleFor(r => r.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount can't be negative");
			RuleFor(r => r.Cost).NotNull().WithMessage("Cost can't be null");
			RuleFor(r => r.TotalValue).NotNull().WithMessage("TotalValue can't be null");

			RuleForEach(r => r.Products).SetValidator(new ProductRequestValidator());
			RuleFor(r => r.Products).Must(r => r.GroupBy(p => p.ProductCategory).Count() == 1).WithMessage("Products must have only one category!");
		}
	}
}

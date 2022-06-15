using FluentValidation;
using RequestDomain.Entities;

namespace RequestDomain.Validators
{
	public class ProductRequestValidator : AbstractValidator<ProductRequest>
	{
		public ProductRequestValidator()
		{
			//RuleFor(p => p.Id).NotNull().WithMessage("Id can't be null");
			//RuleFor(p => p.RequestId).NotNull().WithMessage("RequestId can't be null");
			//RuleFor(p => p.ProductId).NotNull().WithMessage("ProductId can't be null");
			RuleFor(p => p.ProductDescription).NotNull().WithMessage("ProductDescription can't be null");
			RuleFor(p => p.ProductCategory).NotNull().WithMessage("ProductCategory can't be null");
			RuleFor(p => p.Quantity).NotNull().GreaterThan(0).WithMessage("Quantity must be greater than 0");
			RuleFor(p => p.Value).NotNull().GreaterThan(0).WithMessage("Value must be greater than 0");
			RuleFor(p => p.Total).NotNull().GreaterThan(0).WithMessage("Total must be greater than 0");
		}
	}
}

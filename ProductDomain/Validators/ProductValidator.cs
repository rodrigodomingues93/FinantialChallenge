using FluentValidation;
using ProductDomain.Entities;

namespace ProductDomain.Validators
{
	public class ProductValidator : AbstractValidator<Product>
	{
		public ProductValidator()
		{
			RuleFor(p => p.ProductDescription).NotNull().WithMessage("ProductDescription can't be null");
			RuleFor(p => p.ProductCategory).NotNull().WithMessage("ProductCategory can't be null");
			RuleFor(p => p.GTIN).NotNull().WithMessage("Invalid GTIN");
		}
	}
}

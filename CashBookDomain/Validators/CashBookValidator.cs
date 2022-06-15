using CashBookDomain.Entities;
using CashBookDomain.Entities.Enums;
using FluentValidation;

namespace CashBookDomain.Validators
{
    public class CashBookValidator : AbstractValidator<CashBook>
    {
        public CashBookValidator()
        {
            RuleFor(c => c.Origin)
                .IsInEnum().WithMessage("Invalid Origin")
                .NotNull().When(c => c.OriginId != null, ApplyConditionTo.CurrentValidator).WithMessage("OriginId is required for integration!");

            RuleFor(c => c.OriginId)
                .NotNull().When(c => c.Origin == EnumOrigin.BuyRequest || c.Origin == EnumOrigin.Document, ApplyConditionTo.CurrentValidator).WithMessage("OriginId can't be null!");

            RuleFor(c => c.Description).NotEmpty().WithMessage("Description can't be null");
            
            RuleFor(c => c.Type).NotNull().IsInEnum().WithMessage("Invalid Type");
            
            RuleFor(c => c.Value)
                .NotNull().WithMessage("Amount can't be null")
                .LessThan(0).WithMessage("Payment option must have a negative value")
                .When(c => c.Type == EnumType.Payment, ApplyConditionTo.CurrentValidator)
                .GreaterThan(0).WithMessage("Payment option must have a positive value")
                .When(c => c.Type == EnumType.Receipt, ApplyConditionTo.CurrentValidator);
        }
    }
}

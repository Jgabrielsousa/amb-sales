using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    //public class GetSaleCommand : IRequest<GetSaleResult>
    public class GetSaleCommand : IRequest<Result>
    {
        public Guid Id { get; set; } 
       
        public ValidationResultDetail Validate()
        {
            var validator = new GetSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

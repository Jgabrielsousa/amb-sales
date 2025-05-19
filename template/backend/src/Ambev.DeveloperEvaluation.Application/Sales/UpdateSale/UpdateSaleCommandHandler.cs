using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Result>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public UpdateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale  = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
            return new Result(false, $"Sale with ID {command.Id}", null!);

        sale.SaleNumber = command.SaleNumber;
        sale.Customer = command.Customer;
        sale.TotalSaleAmount = command.TotalSaleAmount;
        sale.Branch = command.Customer;
        sale.IsCancelled = command.IsCancelled;

        sale.ItemsClean();
        var newItems = _mapper.Map<List<SaleItemEntity>>(command.SalesItem);
        sale.AddSalesItems(newItems);

        var saleUpdated = await _saleRepository.UpdateAsync(sale, cancellationToken);

        var result = _mapper.Map<UpdateSaleResult>(saleUpdated);

        return new Result(true, "Sale updated successfully", result);
    }
}
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleCommandHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid Sale When Create a Sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var validSaleCommand = GenerateValidCommand();
        var validSaleEntity = ResultSaleEntity();
        var validSaleResult = GenerateValidResult();
        _saleRepository.CreateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>())
            .Returns(validSaleEntity);

        _mapper.Map<SaleEntity>(validSaleCommand).Returns(validSaleEntity);
        _mapper.Map<CreateSaleResult>(validSaleEntity).Returns(validSaleResult);

        // When
        var createUserResult = await _handler.Handle(validSaleCommand, CancellationToken.None);

        // Then
        createUserResult.Should().NotBeNull();
        createUserResult.Success.Should().BeTrue();
    }


    private CreateSaleCommand GenerateValidCommand()
       => new CreateSaleCommand()
       {
           SaleNumber = "1234",
           SaleDate = DateTime.UtcNow.AddDays(-1),
           Customer = "John Doe",
           TotalSaleAmount = 10m,
           Branch = "branch-y",
           SalesItem = new List<CreateSaleItemCommand>() 
           {
                new CreateSaleItemCommand(){ Product = "Uva", Quantity = 10,Discount = 0.20m,TotalAmount = 127.20m,UnitPrice =  15.90m}
           }
       };

    private CreateSaleResult GenerateValidResult()
   => new CreateSaleResult()
   {
       SaleNumber = "1234",
       SaleDate = DateTime.UtcNow.AddDays(-1),
       Customer = "John Doe",
       TotalSaleAmount = 10m,
       Branch = "branch-y",
       SalesItem = new List<CreateSaleItemResult>()
       {
                new CreateSaleItemResult(){ Product = "Uva", Quantity = 10,Discount = 0.20m,TotalAmount = 127.20m,UnitPrice =  15.90m}
       }
   };

    private SaleEntity ResultSaleEntity()
        => new SaleEntity()
        {
            Id = Guid.NewGuid(),
            SaleNumber = "1234",
            SaleDate = DateTime.UtcNow.AddDays(-1),
            Customer = "John Doe",
            TotalSaleAmount = 10m,
            Branch = "branch-y",
            SalesItem = new List<SaleItemEntity>() 
            {
                new SaleItemEntity(){ Product = "Uva", Quantity = 10,Discount = 0.20m,TotalAmount = 127.20m,UnitPrice =  15.90m}
            }
        };
}
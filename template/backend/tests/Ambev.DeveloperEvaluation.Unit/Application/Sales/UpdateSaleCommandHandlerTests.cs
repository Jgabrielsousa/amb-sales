using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class UpdateSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly UpdateSaleCommandHandler _handler;

    public UpdateSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateSaleCommandHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid Sale When Update a Sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var validSaleCommand = GenerateValidCommand();
        var validSaleEntity = ResultSaleEntity();
        var validSaleResult = GenerateValidResult();

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(validSaleEntity);

        _saleRepository.UpdateAsync(Arg.Any<SaleEntity>(), Arg.Any<CancellationToken>())
            .Returns(validSaleEntity);

        _mapper.Map<SaleEntity>(validSaleCommand).Returns(validSaleEntity);
        _mapper.Map<UpdateSaleResult>(validSaleEntity).Returns(validSaleResult);
        _mapper.Map<List<SaleItemEntity>>(validSaleCommand.SalesItem).Returns(GetSaleItemEntityList());

        // When
        var createUserResult = await _handler.Handle(validSaleCommand, CancellationToken.None);

        // Then
        createUserResult.Should().NotBeNull();
        createUserResult.Success.Should().BeTrue();
    }


    private UpdateSaleCommand GenerateValidCommand()
       => new UpdateSaleCommand()
       {
           Id = Guid.NewGuid(),
           SaleNumber = "1234",
           SaleDate = DateTime.UtcNow.AddDays(-1),
           Customer = "John Doe",
           TotalSaleAmount = 10m,
           Branch = "branch-y",
           SalesItem = new List<UpdateSaleItemCommand>() 
           {
                new UpdateSaleItemCommand(){ Product = "Uva", Quantity = 10,Discount = 0.20m,TotalAmount = 127.20m,UnitPrice =  15.90m}
           }
       };

    private UpdateSaleResult GenerateValidResult()
   => new UpdateSaleResult()
   {
       SaleNumber = "1234",
       SaleDate = DateTime.UtcNow.AddDays(-1),
       Customer = "John Doe",
       TotalSaleAmount = 10m,
       Branch = "branch-y",
       SalesItem = new List<UpdateSaleItemResult>()
       {
                new UpdateSaleItemResult(){ Product = "Uva", Quantity = 10,Discount = 0.20m,TotalAmount = 127.20m,UnitPrice =  15.90m}
       }
   };

    public List<SaleItemEntity> GetSaleItemEntityList()
    => new List<SaleItemEntity>()
            {
                new SaleItemEntity(){ Product = "Uva", Quantity = 10,Discount = 0.20m,TotalAmount = 127.20m,UnitPrice =  15.90m}
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
            SalesItem = GetSaleItemEntityList()
        };
}
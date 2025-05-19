using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleCommandHandler _handler;

    public GetSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleCommandHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid Sale Id When Get a Sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = GenerateValidCommand();
        
        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(ResultSaleEntity());
        
        // When
        var createUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createUserResult.Should().NotBeNull();
        createUserResult.Success.Should().BeTrue();
    }


    private GetSaleCommand GenerateValidCommand() 
       => new GetSaleCommand() { Id = Guid.NewGuid() };
    

    private SaleEntity ResultSaleEntity()
        => new SaleEntity()
        {
            Id = Guid.NewGuid(),
            SaleNumber = "1234",
            SaleDate = DateTime.UtcNow,
            Customer = "Joao Silva",
            TotalSaleAmount = 180.00m,
            Branch = "branch-x",
            SalesItem = new List<SaleItemEntity>() {
            new SaleItemEntity(){ Product = "Banana", Quantity = 10,Discount = 10m,TotalAmount = 10m,UnitPrice = 10m}
            }
        };
}
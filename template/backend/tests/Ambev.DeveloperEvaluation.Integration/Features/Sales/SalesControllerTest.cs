using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales;

[Collection("Database")]
public class SalesControllerTest : IClassFixture<Factory>
{
    private readonly Factory _factory;
    private readonly HttpClient _client;
    public SalesControllerTest(Factory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    #region GET METHOD
    [Fact]
    public async Task Given_Sale_When_SaleDoesNotExists_Then_Return_NotFound()
    {
        var response = await _client.GetAsync($"/api/Sales/{Guid.NewGuid()}");
        Assert.NotNull(response);
        Assert.Equal((int)HttpStatusCode.NotFound, (int)response.StatusCode);
    }

    [Fact]
    public async Task Given_Sale_When_SaleDoExists_Then_Return_AValidSale()
    {
        // Arrange
        var sale = await CreateDataBaseSale();

        // Act
        var response = await _client.GetAsync($"/api/Sales/{sale.Id}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal((int)HttpStatusCode.OK, (int)response.StatusCode);
    }


    #endregion

    #region DELETE METHOD
    [Fact]
    public async Task Given_ANotExistSale_When_DeleteSale_Then_Return_NotFound()
    {
        // Act
        var response = await _client.DeleteAsync($"/api/Sales/{Guid.NewGuid()}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal((int)HttpStatusCode.NotFound, (int)response.StatusCode);
    }

    [Fact]
    public async Task Given_AExistSale_When_DeleteSale_Then_Return_Ok()
    {
        // Arrange
        var sale = await CreateDataBaseSale();

        // Act
        var response = await _client.DeleteAsync($"/api/Sales/{sale.Id}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal((int)HttpStatusCode.OK, (int)response.StatusCode);
    }

    #endregion

    #region CREATE METHOD

    [Fact]
    public async Task Given_Sale_When_CreateSale_Then_Return_Ok()
    {
        // Arrange
        var sale = GetSaleCommand();

        // Act
        var response = await _client.PostAsJsonAsync($"/api/Sales", sale);

        // Assert
        Assert.NotNull(response);
        Assert.Equal((int)HttpStatusCode.Created, (int)response.StatusCode);
    }
    #endregion

    #region UPDATE METHOD
    [Fact]
    public async Task Given_Sale_When_UpdateSale_Then_Return_Ok()
    {
        // Arrange
        var sale = await CreateDataBaseSale();
        var updateSale = GetSaleCommand();

        // Act
        var response = await _client.PutAsJsonAsync($"/api/Sales/{sale.Id}", updateSale);
        var saleFromBase = await GetDataBaseSale(sale.Id);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(saleFromBase);
        Assert.Equal((int)HttpStatusCode.Accepted, (int)response.StatusCode);
        Assert.Equal(saleFromBase!.Customer, updateSale.Customer);
        Assert.Equal(saleFromBase.SalesItem!.Count, updateSale.SalesItem!.Count);
        Assert.Equal(saleFromBase.SalesItem!.First().Product, updateSale.SalesItem!.First().Product);
    }
    #endregion

    private async Task<SaleEntity?> GetDataBaseSale(Guid saleId)
    {
        var context = _factory.Services.GetRequiredService<DefaultContext>();

        return await context.Sale
            .AsNoTracking()
            .Include(c => c.SalesItem)
            .FirstOrDefaultAsync(o => o.Id == saleId, default);
    }

    private async Task<SaleEntity> CreateDataBaseSale()
    {
        var context = _factory.Services.GetRequiredService<DefaultContext>();

        var sale = new SaleEntity()
        {
            SaleNumber = "1234",
            SaleDate = DateTime.UtcNow,
            Customer = "Joao Silva",
            TotalSaleAmount = 180.00m,
            Branch = "branch-x",
            SalesItem = new List<SaleItemEntity>() {
            new SaleItemEntity(){ Product = "Banana", Quantity = 10,Discount = 10m,TotalAmount = 10m,UnitPrice = 10m}
            }
        };
        await context.Sale.AddAsync(sale, default);
        await context.SaveChangesAsync(default);
        return sale;
    }

    private CreateSaleCommand GetSaleCommand()
      => new CreateSaleCommand()
      {
          SaleNumber = "1234",
          SaleDate = DateTime.UtcNow.AddDays(-1),
          Customer = "John Doe",
          TotalSaleAmount = 10m,
          Branch = "branch-y",
          SalesItem = new List<CreateSaleItemCommand>() {
            new CreateSaleItemCommand(){ Product = "Uva", Quantity = 10,Discount = 0.20m,TotalAmount = 127.20m,UnitPrice =  15.90m}
            }
      };
}
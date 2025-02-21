using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_repository, _mapper);
    }

    [Fact(DisplayName = "Should create a sale and return its ID when data is valid")]
    public async Task Handle_ValidCommand_ReturnsSaleId()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Sale = new SaleDto { Id = Guid.NewGuid(), Customer = "John Doe", TotalAmount = 100.00m }
        };
        var sale = new Sale() { Id = command.Sale.Id, Customer = command.Sale.Customer, TotalAmount = command.Sale.TotalAmount };

        _mapper.Map<Sale>(command.Sale).Returns(sale);
        _repository.AddAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(sale.Id);
        await _repository.Received(1).AddAsync(Arg.Is<Sale>(s => s.Id == sale.Id));
    }
}

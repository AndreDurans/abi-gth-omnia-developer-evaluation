using Ambev.DeveloperEvaluation.Application.Sales;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SaleDto saleDto)
        {
            var command = new CreateSaleCommand { Sale = saleDto };
            var saleId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetSale), new { id = saleId }, saleId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(Guid id)
        {
            var query = new GetSaleQuery { Id = id };
            var sale = await _mediator.Send(query);
            return sale != null ? Ok(sale) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] SaleDto updatedSaleDto)
        {
            updatedSaleDto.Id = id;
            var command = new UpdateSaleCommand { Sale = updatedSaleDto };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            var command = new CancelSaleCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

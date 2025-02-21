using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class GetSaleQuery : IRequest<SaleDto>
    {
        public Guid Id { get; set; }
    }
}

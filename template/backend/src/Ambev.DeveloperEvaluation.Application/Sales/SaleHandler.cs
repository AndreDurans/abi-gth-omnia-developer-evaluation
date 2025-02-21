using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Sale>(request.Sale);
            await _repository.AddAsync(sale);
            return sale.Id;
        }



        public class GetSaleHandler : IRequestHandler<GetSaleQuery, SaleDto>
        {
            private readonly ISaleRepository _repository;
            private readonly IMapper _mapper;

            public GetSaleHandler(ISaleRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<SaleDto> Handle(GetSaleQuery request, CancellationToken cancellationToken)
            {
                var sale = await _repository.GetByIdAsync(request.Id);
                return _mapper.Map<SaleDto>(sale);
            }
        }


        public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand>
        {
            private readonly ISaleRepository _repository;
            private readonly IMapper _mapper;

            public UpdateSaleHandler(ISaleRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
            {
                var sale = _mapper.Map<Sale>(request.Sale);
                await _repository.UpdateAsync(sale);
            }
        }


        public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
        {
            private readonly ISaleRepository _repository;

            public CancelSaleHandler(ISaleRepository repository)
            {
                _repository = repository;
            }

            public async Task Handle(CancelSaleCommand request, CancellationToken cancellationToken)
            {
                var sale = await _repository.GetByIdAsync(request.Id);
                sale.Cancel();
                await _repository.UpdateAsync(sale);
            }
        }
    }
}

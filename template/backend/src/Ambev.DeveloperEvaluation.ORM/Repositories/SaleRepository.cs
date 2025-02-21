using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<Sale> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                .Include(s => s.Items) // Carrega os itens da venda
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Sale sale)
        {
            var existingSale = await _context.Sales
               .Include(s => s.Items)
               .FirstOrDefaultAsync(s => s.Id == sale.Id);

            if (existingSale == null)
                throw new KeyNotFoundException("Sale not found");

            // Atualiza os dados principais da venda
            _context.Entry(existingSale).CurrentValues.SetValues(sale);

            // Sincroniza SaleItems (adiciona ou remove)
            existingSale.Items.Clear();
            foreach (var item in sale.Items)
            {
                existingSale.Items.Add(new SaleItem(item.Product, item.Quantity, item.UnitPrice, existingSale.Id));
            }

            await _context.SaveChangesAsync();
        }
    }
}

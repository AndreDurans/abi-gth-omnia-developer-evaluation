
namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string Customer { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Branch { get; private set; }
        public List<SaleItem> Items { get; private set; }
        public bool Canceled { get; private set; }

        public Sale(){}

        public Sale(string customer, string branch, List<SaleItem> items)
        {
            Id = Guid.NewGuid();
            SaleDate = DateTime.UtcNow;
            Customer = customer;
            Branch = branch;
            Items = items ?? new List<SaleItem>();
            Canceled = false;
            CalculateTotalAmount();
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = Items.Sum(i => i.TotalAmount);
        }

        public void Cancel()
        {
            Canceled = true;
        }
    }
}

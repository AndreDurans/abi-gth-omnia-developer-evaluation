
namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get;  set; }
        public DateTime SaleDate { get;  set; }
        public string Customer { get;  set; }
        public decimal TotalAmount { get;  set; }
        public string Branch { get;  set; }
        public List<SaleItem> Items { get;  set; }
        public bool Canceled { get;  set; }

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

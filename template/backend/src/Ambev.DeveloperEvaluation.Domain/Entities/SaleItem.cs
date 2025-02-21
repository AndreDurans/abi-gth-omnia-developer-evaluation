using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public string Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public SaleItem(){}

        public SaleItem(string product, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ApplyDiscountRules();
            CalculateTotalAmount();
        }

        public void ApplyDiscountRules()
        {
            if (Quantity >= 4 && Quantity < 10)
                Discount = 0.10m;
            else if (Quantity >= 10 && Quantity <= 20)
                Discount = 0.20m;
            else
                Discount = 0m;

            if (Quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = Quantity * UnitPrice * (1 - Discount);
        }
    }
}

namespace Consimple.Domain.Model.Shop;

public class Order
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
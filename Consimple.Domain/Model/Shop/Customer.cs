namespace Consimple.Domain.Model.Shop;

public class Customer
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public DateOnly BirthDate { get; set; }

    public DateTime RegistrationDate { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
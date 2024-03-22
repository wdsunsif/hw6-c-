
public enum Conditions { NEW, SALE, OLD }

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public Conditions Condition { get; set; }
}

public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

public class Store
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }

    public event EventHandler<string> DiscountEvent;
    public event EventHandler<string> NewCollectionEvent;

    public Store(string name)
    {
        Name = name;
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
        if (product.Condition == Conditions.NEW)
        {
            NewCollectionEvent?.Invoke(this, $"New collection added at {Name}: {product.Name}");
        }
    }

    public void ApplyDiscount(string productName, double discount)
    {
        foreach (var product in Products)
        {
            if (product.Name == productName)
            {
                product.Price -= (product.Price * discount);
                product.Condition = Conditions.SALE;
                DiscountEvent?.Invoke(this, $"Discount applied to {productName} at {Name}");
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var store = new Store("MyStore");
        store.NewCollectionEvent += (sender, message) => Console.WriteLine($"Email sent: {message}");
        store.DiscountEvent += (sender, message) => Console.WriteLine($"SMS sent: {message}");
        store.AddProduct(new Product { Name = "Phone", Price = 1000, Condition = Conditions.NEW });
        store.AddProduct(new Product { Name = "Laptop", Price = 1500, Condition = Conditions.OLD });
        store.ApplyDiscount("Phone", 0.1);

        Console.ReadLine();
    }
}

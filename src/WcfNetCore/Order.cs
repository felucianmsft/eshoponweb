//a class that represent one of the items of the order
using System.Reflection.Metadata;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public virtual Order Order { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, OrderId: {OrderId}, ProductId: {ProductId}, Quantity: {Quantity}, UnitPrice: {UnitPrice}, TotalAmount: {TotalAmount}, Status: {Status}";
    }

}

public class Order {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingState { get; set; }
    public string ShippingZipCode { get; set; }
    public string ShippingCountry { get; set; }

    public virtual ICollection<OrderItem> Items { get; set; }
  
    public override string ToString()
    {
        var items = "";
        if(Items != null)
        {
            foreach (var item in Items)
            {
                items += "\n-item-" + item.ToString();
            }
        }

        return $"Id: {Id}, CustomerId: {CustomerId}, OrderNumber: {OrderNumber}, OrderDate: {OrderDate}, TotalAmount: {TotalAmount}, Status: {Status}, ShippingAddress: {ShippingAddress}, ShippingCity: {ShippingCity}, ShippingState: {ShippingState}, ShippingZipCode: {ShippingZipCode}, ShippingCountry: {ShippingCountry} + Items {items}";
    }   
}

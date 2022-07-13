//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("quantity_product")]
public class QuantityProduct
{
    [Key]
    public int QuantityProductID { get; set; }

    public int Quantity { get; set; }

    public int OrderID { get; set; }
    public Order Order { get; set; }

    public int ProductID { get; set; }
    public Product Product { get; set; }


    public QuantityProduct(int quantity, Product product, Order order)
    {
        this.Order = order;
        this.OrderID = order.OrderID;
        this.Product = product;
        this.ProductID = product.Id;
        this.Quantity = quantity;
    }

    public QuantityProduct(int quantity, Product product)
    {
        this.Quantity = quantity;
        this.Product = product;
        this.ProductID = product.Id;
    }
    public QuantityProduct()
    {

    }
}


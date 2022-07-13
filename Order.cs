//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("order")]
public class Order
{
    [Key]
    public int OrderID { get; set; }    

    public DateTime Date { get; set; }

    public double Amount { get; set; }

    public bool Status { get; set; }

    [Required]
    public int CustomerID { get; set; }
    public Customer Customer { get; set; }

    public List<QuantityProduct> QuantityProduct { get; set; }


    public Order( Customer customer, List<Product> products)
    {
        this.Customer = customer;
        this.CustomerID = customer.CustomerID;
        this.Status = true;
        foreach(Product product in products)
        {
            this.Amount += product.Price;
        }
    }

    public Order()
    {

    }


    public void AddProducts(List<Product> products)
    {
        foreach (Product product in products)
        {
            this.Amount += product.Price;
        }
    }
}



//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("customer")]
public class Customer
{
    [Key]
    public int CustomerID { get; set; }

    public string CustomerName { get; set; }

    [Column("customer_email")]
    public string Email { get; set; }

    public List<Order> Order { get; set; }

    public List<QuantityProduct> QuantityProduct { get; set; }

    public Customer(string customerName)
    {
        this.CustomerName = customerName;
    }

}


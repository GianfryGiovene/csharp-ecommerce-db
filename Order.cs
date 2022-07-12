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

    public DateOnly Date { get; set; }

    public double Amount { get; set; }

    public bool Status { get; set; }

    [Required]
    public int CustomerID { get; set; }
    public Customer Customer { get; set; }

    public List<QuantityProduct> QuantityProduct { get; set; }
}



//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("product")]
public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product( string name, double price)
    {
        Name = name;
        Price = price;
    }
}


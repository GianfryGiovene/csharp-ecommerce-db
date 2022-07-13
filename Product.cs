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
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }

    public List<QuantityProduct> QuantityProducts { get; set; }

    public Product( string name, string description, double price)
    {
        this.Name = name;
        this.Description = description;
        this.Price = price;
    }
}


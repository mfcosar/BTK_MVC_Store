using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;

namespace Entities.Models;
public class Product
{
    public int ProductId { get; set; }
    
    public string? ProductName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public String? Summary { get; set;} = string.Empty;

    public String? ImageUrl { get; set; }

    public int? CategoryId { get; set; } //Foreign key
    public Category? Category {  get; set; } //Navigation property
    public bool ShowCase { get; set; } //Vitrin

}

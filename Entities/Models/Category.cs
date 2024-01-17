using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        // solid yazılımda bu entity'nin başka sorumluluğu olmamalı. Bu yüzden kapattık
        //[Required(ErrorMessage = "Category name is required")]
        public String? CategoryName { get; set; } = String.Empty;

        public ICollection<Product>? Products { get; set; }  // Collection navigation property -
        // nullable sonradan eklendi yoksa form validation required field görüyor

    }
}

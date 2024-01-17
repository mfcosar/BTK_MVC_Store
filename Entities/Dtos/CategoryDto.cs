using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record CategoryDto
    {
        // immutable'dır, değişmez. set yerine init ile variable'lar tanımlanır
        public int CategoryId { get; init; }

        [Required(ErrorMessage = "Category name is required")]
        //validation işlemleri dto'a aktarılır
        public String? CategoryName { get; init; } = String.Empty;

        

    }
}

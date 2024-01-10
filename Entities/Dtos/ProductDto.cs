﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record ProductDto
    {
        public int ProductId { get; init; }

        [Required(ErrorMessage = "Product Name is required")]
        public string? ProductName { get; init; } = string.Empty;

        [Required(ErrorMessage = "Product price is required")]
        public decimal Price { get; init; }
        public String? Summary { get; init; } = string.Empty;
        public String? ImageUrl { get; set; }
        public int? CategoryId { get; init; } 
        //public Category? Category { get; set; } //ihtiyaç yok bu record type'da , onun için kapattık
    }
}

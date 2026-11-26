using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ProductInputDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = "";

    public string? Description { get; set; }

    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}

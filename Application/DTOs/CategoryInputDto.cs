using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CategoryInputDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [StringLength(255)]
    public string? Description { get; set; }
}

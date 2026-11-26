using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record ProductDetailDto
    (
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock,
        string Category,
        int CategoryId
    );
}

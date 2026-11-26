using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record ProductListItemDto
    (
        int Id,
        string Name,
        decimal Price,
        int Stock,
        string Category,
        int CategoryId
    );
}

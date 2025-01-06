using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CategoryDTO
    {
        [Required]
        public string CategoryName { get; set; } = null!;
        public int CategoryId { get; set; } = 0;
    }
}

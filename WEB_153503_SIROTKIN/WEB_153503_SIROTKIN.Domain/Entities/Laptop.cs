using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153503_SIROTKIN.Domain.Entities
{
    public class Laptop
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }

    }


}

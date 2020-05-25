using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SomeWebShowroom.MVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IEnumerable<UserProducts> UserProducts { get; set; } = new List<UserProducts>();
    }
}

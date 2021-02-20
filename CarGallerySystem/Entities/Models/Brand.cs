using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarGallerySystem.Entities.Models
{
    public class Brand:BaseHome
    {
        public string Name { get; set; }
        public string Photo { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<Model> Models { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
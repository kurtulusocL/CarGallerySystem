using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarGallerySystem.Entities.Models
{
    public class Model : BaseHome
    {
        public string Name { get; set; }

        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
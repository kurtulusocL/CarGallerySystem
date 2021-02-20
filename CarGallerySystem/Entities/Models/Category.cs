using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarGallerySystem.Entities.Models
{
    public class Category : BaseHome
    {
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
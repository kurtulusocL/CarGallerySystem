using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarGallerySystem.Entities.Models
{
    public class Car : BaseHome
    {
        public string Plaka { get; set; }
        public int Year { get; set; }
        public string Colour { get; set; }
        public double KM { get; set; }
        public int TotalCar { get; set; }
        public decimal? Price { get; set; }
        public bool IsAvaible { get; set; }

        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Contract> Contracts { get; set; }

        public Car()
        {
            IsAvaible = true;
        }
    }
}
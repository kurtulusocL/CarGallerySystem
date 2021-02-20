using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarGallerySystem.Entities.Models
{
    public class Customer : BaseHome
    {
        public string IdentityNo { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }

        public int? CarId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }

        public ICollection<Contract> Contracts { get; set; }
    }
}
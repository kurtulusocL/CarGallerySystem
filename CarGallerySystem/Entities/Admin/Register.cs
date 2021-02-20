using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarGallerySystem.Entities.Admin
{
    public class Register
    {
        [Required]
        [Display(Name = "Ad Soyad")]
        public string NameLastname { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Doğum Tarihiniz")]
        public string Birthdate { get; set; }

        [Required]
        [Display(Name = "Telefon No")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Göreviniz")]
        public string RespondTitle { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]

        public string ConfirmPassword { get; set; }
    }
}
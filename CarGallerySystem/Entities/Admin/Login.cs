﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarGallerySystem.Entities.Admin
{
    public class Login
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}